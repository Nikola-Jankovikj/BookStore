using BookStore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Odbc;

namespace BookStore.Web.Controllers
{
    public class ExternalController : Controller
    {
        private readonly string _connectionString = "Driver={ODBC Driver 18 for SQL Server};Server=tcp:charityplatformweb111dbserver.database.windows.net,1433;Database=CharityPlatform.Web_db111;Uid=charity123;Pwd=Donation123;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;";

        public async Task<IActionResult> Index()
        {
            List<DonationDto> donations = new List<DonationDto>();
            using (OdbcConnection connection = new OdbcConnection(_connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();
                    Console.WriteLine("Connection to the database is open.");
                    string query = "SELECT * FROM DONATIONS";
                    using(OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        using(OdbcDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                DonationDto donation = new DonationDto
                                {
                                    Amount = reader.GetDouble(1),
                                    DonationDate = DateOnly.FromDateTime(reader.GetDateTime(2)), 
                                };

                                donations.Add(donation);
                            }

                        }
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"An error occurred while opening the connection: {ex.Message}");
                }
                finally
                {
                    // Close the connection
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                        Console.WriteLine("Connection to the database is closed.");
                    }
                }
            }
            donations = donations.OrderBy(d => d.Amount).ToList();
            return View(donations);
        }
    

    }
}

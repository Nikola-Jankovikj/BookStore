﻿using BookStore.Domain.Domain;
using BookStore.Service.Implementation;
using BookStore.Service.Interface;
using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace BookStore.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController (IOrderService orderService)
        {
            _orderService = orderService;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Redirect("Identity/Account/Login");
            }

            var orders = _orderService.GetAllOrdersByUser(userId);

            return View(orders);
        }

        public IActionResult Details(Guid id)
        {
            var order = _orderService.GetDetailsForOrder(id);
            return View(order);
        }

        [HttpGet]
        public FileContentResult ExportAllOrders()
        {
            string fileName = "AllOrders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workBook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("Orders");

                worksheet.Cell(1, 1).Value = "No.";
                worksheet.Cell(1, 2).Value = "Order ID";
                worksheet.Cell(1, 3).Value = "Time";
                worksheet.Cell(1, 4).Value = "Owner";

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var data = _orderService.GetAllOrdersByUser(userId);

                for (int i = 0; i < data.Count(); i++)
                {
                    var order = data[i];
                    worksheet.Cell(i + 2, 1).Value = (i+1).ToString();
                    worksheet.Cell(i + 2, 2).Value = order.Id.ToString();
                    worksheet.Cell(i + 2, 3).Value = order.OrderDate.Date.ToShortDateString() + ", " + order.OrderDate.ToShortTimeString();
                    worksheet.Cell(i + 2, 4).Value = order.Owner.Email;

                    for (int j = 0; j < order.BooksInOrder.Count(); j++)
                    {
                        worksheet.Cell(1, j + 5).Value = "Book " + (j + 1);
                        worksheet.Cell(i + 2, j + 5).Value = "\"" + order.BooksInOrder.ElementAt(j).Book.Title + "\" by " + order.BooksInOrder.ElementAt(j).Book.Author.Name;

                    }
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }

        }

    }
}

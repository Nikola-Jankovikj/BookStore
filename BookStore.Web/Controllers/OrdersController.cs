using BookStore.Domain.Domain;
using BookStore.Service.Implementation;
using BookStore.Service.Interface;
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

        public FileContentResult CreateInvoice(Guid id)
        {
            var order = _orderService.GetDetailsForOrder(id);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "InvoiceOneOrder.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderNumber}}", order.Id);
            document.Content.Replace("{{OwnerEmail}}", order.OwnerEmail);

            StringBuilder sb = new StringBuilder();
            var total = 0.0;
            foreach (var item in order.Books)
            {
                sb.AppendLine("\"" + item.Book.Title + "\" by " + item.Book.Author.Name + " - Quantity: " + item.Quantity + " - Price: " + item.Book.Price + "$");
                total += (item.Quantity * item.Book.Price);
            }
            document.Content.Replace("{{OrderTotalPrice}}", total.ToString() + "$");
            document.Content.Replace("{{BooksInOrder}}", sb.ToString());

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");

        }
    }
}

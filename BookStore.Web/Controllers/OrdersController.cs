using BookStore.Service.Implementation;
using BookStore.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController (IOrderService orderService)
        {
            _orderService = orderService;
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
    }
}

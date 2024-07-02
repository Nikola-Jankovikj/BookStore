using BookStore.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Stripe;

namespace BookStore.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {

        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService _shoppingCartService)
        {
            this._shoppingCartService = _shoppingCartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Redirect("Identity/Account/Login");
            }

            var dto = _shoppingCartService.getShoppingCartInfo(userId);

            return View(dto);
        }

        public IActionResult DeleteFromShoppingCart(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartService.deleteBookFromShoppingCart(userId, id);
            
            return RedirectToAction("Index", "ShoppingCarts");
        }

        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var res = _shoppingCartService.order(userId);
            
            return RedirectToAction("Index", "ShoppingCarts");

        }

        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult NotSuccessPayment()
        {
            return View();
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51Io84IHBiOcGzrvu4sxX66rTHq8r5nxIxRiJPbOHB4NwVJOE1jSlxgYe741ITs024uXhtpBFtxm3RoCZc3kafocC00IhvgxkL0";
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "Bookstore Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.Order();
                return RedirectToAction("SuccessPayment");

            }
            else
            {
                return RedirectToAction("NotSuccessPayment");
            }
        }
    }
}

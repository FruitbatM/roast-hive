using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoastHiveMvc.Models;
using System.Globalization;
using RoastHiveMvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace RoastHiveMvc.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        private void UpdateCart(Cart cart)
        {
            HttpContext.Session.SetAsJson("Cart", cart);
        }


        private Cart GetShoppingCart()
        {
            // Try and get the cart from the session.
            var cart = HttpContext.Session.GetFromJson<Cart>("Cart");

            // If one doesn't exist, create a new one
            if (cart == null)
            {
                cart = new Cart();
                UpdateCart(cart);
            }

            return cart;
        }

        [HttpGet]
        [Route("api/Cart/TotalAmount")]
        public IActionResult TotalAmount()
        {
            var cart = GetShoppingCart();
            double totalAmount = cart.Items.Sum(item => item.Quantity * item.UnitPrice);
            string formattedAmount = totalAmount.ToString("C", CultureInfo.GetCultureInfo("en-IE"));
            return Content(formattedAmount);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int itemId, int quantity)
        {
            var cart = GetShoppingCart();
            cart.UpdateQuantity(itemId, quantity);
            UpdateCart(cart);

            double totalAmount = cart.Items.Sum(item => item.Quantity * item.UnitPrice);
            string formattedAmount = totalAmount.ToString("C", CultureInfo.GetCultureInfo("en-IE"));

            return Json(new { success = true, totalAmount = formattedAmount });
        }


        [HttpPost]
        public IActionResult Remove(int itemId)
        {
            var cart = GetShoppingCart();
            cart.Remove(itemId);
            UpdateCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = GetShoppingCart();
            ViewBag.CartTotal = TotalAmount();

            return View(cart);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
        }
    }
}
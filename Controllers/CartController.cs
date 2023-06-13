using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoastHiveMvc.Models;
using System.Globalization;
using Newtonsoft.Json;
using RoastHiveMvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace RoastHiveMvc.Controllers
{
    public class CartController : Controller
    {
        public class TotalAmountResponse
        {
            public string? cartTotal { get; set; }
        }

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
            // changed decimal to double and culture to IE
            var cart = GetShoppingCart();
            int itemCount = cart.Items.Sum(item => item.Quantity);
            return Content(itemCount.ToString());
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int itemId, int quantity)
        {
            var cart = GetShoppingCart();
            cart.UpdateQuantity(itemId, quantity);
            UpdateCart(cart);

            int itemCount = cart.Items.Sum(item => item.Quantity);
            string cartTotal = itemCount.ToString();

            return RedirectToAction("Index", new { cartTotal });
        }

        [HttpPost]
        public IActionResult Remove(int itemId)
        {
            var cart = GetShoppingCart();
            cart.Remove(itemId);
            UpdateCart(cart);

            int itemCount = cart.Items.Sum(item => item.Quantity);
            string cartTotal = itemCount.ToString();

            return RedirectToAction("Index", new { cartTotal });
        }

        public IActionResult Index(string cartTotal)
        {
            var cart = GetShoppingCart();
            ViewBag.CartTotal = cartTotal ?? "0";
            return View(cart);
        }

        [HttpPost]
        public IActionResult UpdateCartTotal(string cartTotal)
        {
            ViewBag.CartTotal = cartTotal;
            return Json(new { success = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
        }
    }
}

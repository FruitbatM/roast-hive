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
            public string cartTotal { get; set; }
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
            var cart = GetShoppingCart();
            double totalAmount = cart.Items.Sum(item => item.Quantity * item.UnitPrice);
            string formattedAmountWithSymbol = "€" + totalAmount.ToString("N2", CultureInfo.GetCultureInfo("ie-IE"));
            return Content(formattedAmountWithSymbol);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int itemId, int quantity)
        {
            var cart = GetShoppingCart();
            cart.UpdateQuantity(itemId, quantity);
            UpdateCart(cart);

            double totalAmount = cart.Items.Sum(item => item.Quantity * item.UnitPrice);
            string formattedAmountWithSymbol = "€" + totalAmount.ToString("N2", CultureInfo.GetCultureInfo("ie-IE"));

            ViewBag.CartTotal = formattedAmountWithSymbol;

            return RedirectToAction("Index", new { cartTotal = formattedAmountWithSymbol });
        }

        [HttpPost]
        public IActionResult Remove(int itemId)
        {
            var cart = GetShoppingCart();
            cart.Remove(itemId);
            UpdateCart(cart);

            double totalAmount = cart.Items.Sum(item => item.Quantity * item.UnitPrice);
            string formattedAmountWithSymbol = "€" + totalAmount.ToString("N2", CultureInfo.GetCultureInfo("ie-IE"));

            ViewBag.CartTotal = formattedAmountWithSymbol;

            return RedirectToAction("Index", new { cartTotal = formattedAmountWithSymbol });
        }

        public IActionResult Index(string cartTotal)
        {
            var cart = GetShoppingCart();

            if (!string.IsNullOrEmpty(cartTotal))
            {
                ViewBag.CartTotal = cartTotal;
            }
            else
            {
                // Make an HTTP request to retrieve the cart total amount
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = client.GetAsync("/api/Cart/TotalAmount").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseData = response.Content.ReadAsStringAsync().Result;
                            var totalAmountResponse = JsonConvert.DeserializeObject<TotalAmountResponse>(responseData);
                            string formattedAmountWithSymbol = totalAmountResponse.cartTotal;
                            ViewBag.CartTotal = formattedAmountWithSymbol;
                        }
                        else
                        {
                            // Handle the case when the request to TotalAmount endpoint fails
                            ViewBag.CartTotal = "€0.00";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception when the request fails
                        ViewBag.CartTotal = "€0.00";
                        _logger.LogError(ex, "Error retrieving cart total amount.");
                    }
                }
            }

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
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
        }
    }
}

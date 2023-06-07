using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoastHiveMvc.Models;
using RoastHiveMvc;

namespace RoastHiveMvc.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<ContactController> _logger;

        public CartController(ILogger<ContactController> logger)
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
        [Route("api/Cart/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var cart = GetShoppingCart();
            await Task.Run(() => cart.Remove(id));
            UpdateCart(cart);
            return View(cart);
        }

        public IActionResult Index()
        {
            var cart = GetShoppingCart();
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
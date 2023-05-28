using Microsoft.AspNetCore.Mvc;
using RoastHiveMvc.Models;

namespace RoastHiveMvc.Controllers
{
    // [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {          
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.UnitPrice * item.Quantity);
                ViewBag.total = Math.Round(ViewBag.total, 2);
                return View();
        }

        // private string GetDebuggerDisplay()
        // {
        //     return ToString();
        // }
    }
}

using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Globalization;
using RoastHiveMvc.Models;
using RoastHiveMvc.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using RoastHiveMvc;

namespace RoastHiveMvc.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ShopController : Controller
{
    private readonly RoastHiveDbContext _db;
    public ShopController(RoastHiveDbContext db)
    {
        _db = db;
    }

    [HttpGet]

    public async Task<IActionResult> Index()
    {
        return _db.Product != null ?
                    View(await _db.Product.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.Product'  is null.");
    }

    private void UpdateCart(Cart cart)
    {
        HttpContext.Session.SetAsJson("Cart", cart);
    }

    private Cart GetShoppingCart()
    {
        var cart = HttpContext.Session.GetFromJson<Cart>("Cart");

        if (cart == null)
        {
            cart = new Cart();
            HttpContext.Session.SetAsJson("Cart", cart);
        }
        if (cart == null)
        {
            cart = new Cart();
            UpdateCart(cart);
        }
        return cart;
    }

    [HttpGet]
    public async Task<IActionResult> AddToCart(int ProdID, string Name, int Quantity, double UnitPrice, string Url)
    {
        var cart = GetShoppingCart();

        var item = new CartItem()
        {
            Name = Name,
            ProductId = ProdID,
            Quantity = Quantity,
            UnitPrice = UnitPrice,
            Url = Url ?? "~/images/mug.png" // Default URL if Url is null
        };

        cart.Add(item);
        UpdateCart(cart);
        return RedirectToAction("Index");
    }

    // GET: api/Shop/Details/1
    // Product details
    [HttpGet]
    [Route("api/Shop/Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _db.Product == null)
        {
            return NotFound();
        }

        var product = await _db.Product
            .FirstOrDefaultAsync(m => m.ProdID == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // Search Products
    [HttpGet]
    public async Task<IActionResult> Filter(string searchString)
    {
        var allProducts = await _db.Product.ToListAsync();

        if (!string.IsNullOrEmpty(searchString))
        {
            var filteredResult = allProducts.Where(m =>
                m.Name?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true ||
                m.Description?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true
        ).ToList();

            return View("Index", filteredResult);
        }

        return View("Index", allProducts);
    }

    // Filter products by category
    [HttpGet]
    public async Task<IActionResult> FilterByCategory(string category)
    {
        var allProducts = await _db.Product.ToListAsync();

        if (!string.IsNullOrEmpty(category))
        {
            var filteredResult = allProducts.Where(m =>
                string.Equals(m.CatId, category, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            return View("Index", filteredResult);
        }

        return View("Index", allProducts);
    }
}

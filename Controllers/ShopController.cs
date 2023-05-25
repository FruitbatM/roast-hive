using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RoastHiveMvc.Models;
using RoastHiveMvc.Data;

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

    // GET: Product
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return _db.Product != null ?
                    View(await _db.Product.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.Product'  is null.");
    }

    // GET: Shop/Details/1
    [HttpGet]
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
    public async Task<IActionResult> Filter(string searchString)
    {
        var allProducts = await _db.Product.ToListAsync();

        if (!string.IsNullOrEmpty(searchString))
        {
            var filteredResult = allProducts.Where(m => m.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) || m.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

            return View("Index", filteredResult);
        }

        return View("Index", allProducts);
    }

    /*public async Task<IActionResult> Index()
        {
            IAsyncEnumerable<Product> objProductList = (IAsyncEnumerable<Product>)_db.Product;
            return View(objProductList);
        }
    */
}

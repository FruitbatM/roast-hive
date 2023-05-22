using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RoastHiveMvc.Models;
using RoastHiveMvc.Data;

namespace RoastHiveMvc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopController : Controller
{
    private readonly ProductsDbContext _db;
    public ShopController(ProductsDbContext db)
    {
        _db = db;
    }

    // GET: Product

    public async Task<IActionResult> Index()
        {
              return _db.Product != null ? 
                          View(await _db.Product.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Product'  is null.");
        }
    
    /*public async Task<IActionResult> Index()
        {
            IAsyncEnumerable<Product> objProductList = (IAsyncEnumerable<Product>)_db.Product;
            return View(objProductList);
        }
    */
}

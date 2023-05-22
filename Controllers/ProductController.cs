using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RoastHiveMvc.Models;
using RoastHiveMvc.Data;

namespace RoastHiveMvc.Controllers;

//Controller for Product Page
[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly ProductsDbContext _db;
    public ProductController(ProductsDbContext db)
    {
        _db = db;
    }


    // GET: Product/Details/5
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
}

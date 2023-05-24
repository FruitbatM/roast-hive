using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using RoastHiveMvc.Models;
using RoastHiveMvc.Data;

namespace RoastHiveMvc.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DetailsController : Controller
{
    private readonly RoastHiveDbContext _context;

    public DetailsController(RoastHiveDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
        {
              return _context.Product != null ? 
                          View(await _context.Product.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Product'  is null.");
        } 
    // GET: Product Details
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Product == null)
        {
            return NotFound();
        }

        var product = await _context.Product
            .FirstOrDefaultAsync(m => m.ProdID == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

}

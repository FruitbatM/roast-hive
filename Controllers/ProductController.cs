using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RoastHiveMvc.Models;
using RoastHiveMvc.Data;

namespace RoastHiveMvc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly RoastHiveDbContext _context;

    public ProductController(RoastHiveDbContext context)
    {
        _context = context;
    }

    // GET: Product Details
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

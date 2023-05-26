using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoastHiveMvc.Models;
using RoastHiveMvc.Data;
using System.Linq;

namespace RoastHiveMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RoastHiveDbContext _db;

    public HomeController(ILogger<HomeController> logger, RoastHiveDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    /*
    public IActionResult Index()
    {
        return View();
    }
    */

    public IActionResult Index(string category)
    {
        // Retrieve three products from the specified category
        var products = _db.Product
            .Where(p => p.CatId == "Espresso")
            .Take(3)
            .ToList();

        // Pass the products to the view
        return View(products);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

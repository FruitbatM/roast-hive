using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoastHiveMvc.Data;
using RoastHiveMvc.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace RoastHiveMvc.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Administrator")]
    public class ManageProductController : Controller
    {
        private readonly RoastHiveDbContext _db;
        private const string DefaultImageUrl = "https://res.cloudinary.com/fruitbat/image/upload/v1686403354/Roast%20Hive/default_kzubh0.png";

        public ManageProductController(RoastHiveDbContext context)
        {
            _db = context;
        }

        // GET including sorting functionality
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Index(string? sortOrder)
        {
            if (_db.Product == null)
            {
                return NotFound();
            }

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            var products = from p in _db.Product
                           select p;

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Category":
                    products = products.OrderBy(p => p.CatId);
                    break;
                case "category_desc":
                    products = products.OrderByDescending(p => p.CatId);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.CatId);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.UnitPrice);
                    break;
                default:
                    products = products.OrderBy(p => p.UnitPrice);
                    break;
            }

            return View(await products.AsNoTracking().ToListAsync());

        }

        // APIs to create, edit and delete products
        // To display category names from the dropdown list, create a list of SelectListItem objects and pass it to the view
        [HttpGet]
        public IActionResult Create()
        {
            var categories = _db.Product.Select(p => p.CatId).Distinct().ToList();
            var selectListItems = categories.Select(category => new SelectListItem
            {
                Text = category,
                Value = category
            }).ToList();

            ViewBag.Categories = selectListItems;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<IEnumerable<Product>>> Create([Bind("CatId, Name, Description, Size, UnitPrice, Origin, Url")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(product.Url))
                {
                    product.Url = DefaultImageUrl;
                }

                _db.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(new Product());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Edit(int? id)
        {
            if (id == null || _db.Product == null)
            {
                return NotFound();
            }

            var product = await _db.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEdit(int id, [Bind("ProdID, CatId, Name, Description, Size, UnitPrice, Origin, Url")] Product product)
        {
            if (id != product.ProdID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(product);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProdID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostDelete(int? id)
        {
            var product = _db.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            _db.Product.Remove(product);
            _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Async DELETE
        /* [HttpDelete("{id:int}")]
        public async Task<IActionResult> ProductDelete(int? id)
        {
            var product = await _db.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            
            _db.Product.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }  */
        private bool ProductExists(int id)
        {
            return (_db.Product?.Any(e => e.ProdID == id)).GetValueOrDefault();
        }


        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoastHiveMvc.Data;
using RoastHiveMvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace RoastHiveMvc.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Administrator")]
    public class ManageProductController : Controller
    {
        private readonly RoastHiveDbContext _db;

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

        // Create new product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<IEnumerable<Product>>> Create([Bind("CatId, Name, Description, Size, UnitPrice, Origin, Url")] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(new Product());
        }

        // Edit/Update product
        //[HttpPut("{id}")]
        [HttpGet]
        [Authorize]
        /* public async Task<ActionResult<IEnumerable<Product>>> Edit(int id, Product product)
        {
            if (id != product.ProdID){
                return BadRequest();
            }

            _db.Entry(product).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        } */
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

        // POST: Edit/Update product
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

        /* private bool ProductExists(int? id)
        {
            return _db.Product.Any(e => e.ProdID == id);
        } */

         // GET: Delete product
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
        /* [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(int id)
        {
            if (_db.Product == null)
            {
                return Problem("Entity set 'ProductsDbContext.Product'  is null.");
            }
            var product = await _db.Product.FindAsync(id);
            if (product != null)
            {
                _db.Product.Remove(product);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } */
        // Delete product
        
        /* [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_db.Product == null)
            {
                return Problem("Entity set 'ProductsDbContext.Product'  is null.");
            }
            var product = await _db.Product.FindAsync(id);
            if (product != null)
            {
                _db.Product.Remove(product);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }  

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_db.Product == null)
            {
                return NotFound();
            }
            var product = await _db.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _db.Product.Remove(product);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        } */
        private bool ProductExists(int id)
        {
            return (_db.Product?.Any(e => e.ProdID == id)).GetValueOrDefault();
        } 
    }
}

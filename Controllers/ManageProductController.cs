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
    [ApiController]
    public class ManageProductController : Controller
    {
        private readonly ProductsDbContext _context;

        public ManageProductController(ProductsDbContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
              return _context.Product != null ? 
                          View(await _context.Product.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Product'  is null.");
        }

        // GET: Product/ShowSearchForm

        /*public async Task<IActionResult> ShowSearchForm()
        {
            return _context.Products != null ?
                        View() :
                        Problem("Entity set 'ProductsDbContext.Product'  is null.");
        }
        // POST: Product/ShowSearchResults
        
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Products.Where(j => j.Name.Contains
            (SearchPhrase)).ToListAsync());
        }
        */

        // GET: Create new product
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create new product
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Created([Bind("ProdID, CatId, Name, Description, Size, UnitPrice, Origin")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        
        // GET: Edit/Update product
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Edit/Update product
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edited(int id, [Bind("ProdID, CatId, Name, Description, Size, UnitPrice, Origin")] Product product)
        {
            if (id != product.ProdID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
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

        private bool ProductExists(int? id)
        {
            throw new NotImplementedException();
        }

        // GET: Delete product
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
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

        // POST: Delete product
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ProductsDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProdID == id)).GetValueOrDefault();
        }
    }
}

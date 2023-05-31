using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoastHiveMvc.Data;
using Microsoft.AspNetCore.Authorization;
using RoastHiveMvc.Models;

namespace JRoastHiveMvc.Controllers
{
    public class CartController : Controller
    {
        private readonly RoastHiveDbContext _context;

        public CartController(RoastHiveDbContext context)
        {
            _context = context;
        }

        public ActionResult AddToCart(int CatID, int ProdID, decimal UnitPrice, int Quantity)
        {
            // Retrieve the cart items from the session or database
            List<CartItem> cartItems = GetCartItemsFromSession();

            // Check if the product is already in the cart
            CartItem existingItem = cartItems.FirstOrDefault(item => item.ProdID == ProdID);
            if (existingItem != null)
            {
                existingItem.Quantity += Quantity;
            }
            else
            {
                // Add the new item to the cart
                CartItem newItem = new CartItem
                {
                    CartId = CatID,
                    ProdID = ProdID,
                    UnitPrice = UnitPrice,
                    Quantity = Quantity,
                    // Set other properties like ProductName and Price
                };
                cartItems.Add(newItem);
            }

    // Save the updated cart items back to the session or database

    return RedirectToAction("ViewCart");
}

        private List<CartItem> GetCartItemsFromSession()
        {
            throw new NotImplementedException();
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
              return _context.Item != null ? 
                          View(await _context.Product.ToListAsync()) :
                          Problem("Entity set 'RoastHiveDbContext.Product'  is null.");
        }

        // GET: ShowProductForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: ShowProductsResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Product.Where(j=> j.Description.Contains(SearchPhrase)).ToListAsync());

        }
        // GET: Products/Details/5
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

        // GET: Products/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: product/Edit/5
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

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Product product)
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

        // GET: Products/Delete/5
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

        // POST: product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
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

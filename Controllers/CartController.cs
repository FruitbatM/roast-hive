using Microsoft.AspNetCore.Mvc;
using RoastHiveMvc.Models;

namespace RoastHiveMvc.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.UnitPrice * item.Quantity);
            ViewBag.total = Math.Round(ViewBag.total, 2);
            return View();
        }

       private int isExist(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if(cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1; 
        }
       public IActionResult Buy(string id)
        {
            ProductModel productModel = new ProductModel();
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
    }
}



// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using RoastHiveMvc.Data;
// using RoastHiveMvc.Models;
// using Microsoft.AspNetCore.Authorization;

// namespace RoastHiveMvc.Controllers
// {
//     public class CartController : Controller
//     {
//         private readonly RoastHiveDbContext _context;

//         public CartController(RoastHiveDbContext context)
//         {
//             _context = context;
//         }

//         public ActionResult AddToCart(int CatID, int ProdID, decimal UnitPrice, int Quantity)
//         {
//             // Retrieve the cart items from the session or database
//             List<CartItem> cartItems = GetCartItemsFromSession();

//             // Check if the product is already in the cart
//             CartItem existingItem = cartItems.FirstOrDefault(item => item.ProdID == ProdID);
//             if (existingItem != null)
//             {
//                 existingItem.Quantity += Quantity;
//             }
//             else
//             {
//                 // Add the new item to the cart
//                 CartItem newItem = new CartItem
//                 {
//                     CartId = CatID,
//                     ProdID = ProdID,
//                     UnitPrice = UnitPrice,
//                     Quantity = Quantity,
//                     // Set other properties like ProductName and Price
//                 };
//                 cartItems.Add(newItem);
//             }

//     // Save the updated cart items back to the session or database

//     return RedirectToAction("ViewCart");
// }

//         private List<CartItem> GetCartItemsFromSession()
//         {
//             throw new NotImplementedException();
//         }

//         // GET: Items
//         public async Task<IActionResult> Index()
//         {
//               return _context.Item != null ? 
//                           View(await _context.Joke.ToListAsync()) :
//                           Problem("Entity set 'ApplicationDbContext.Joke'  is null.");
//         }

//         // GET: ShowJokesForm
//         public async Task<IActionResult> ShowSearchForm()
//         {
//             return View();
//         }

//         // POST: ShowJokesResults
//         public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
//         {
//             return View("Index", await _context.Joke.Where(j=> j.JokeQuestion.Contains(SearchPhrase)).ToListAsync());

//         }
//         // GET: Jokes/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null || _context.Joke == null)
//             {
//                 return NotFound();
//             }

//             var joke = await _context.Joke
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (joke == null)
//             {
//                 return NotFound();
//             }

//             return View(joke);
//         }

//         // GET: Jokes/Create
//         [Authorize]
//         public IActionResult Create()
//         {
//             return View();
//         }

//         // POST: Jokes/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(joke);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(joke);
//         }

//         // GET: Jokes/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null || _context.Joke == null)
//             {
//                 return NotFound();
//             }

//             var joke = await _context.Joke.FindAsync(id);
//             if (joke == null)
//             {
//                 return NotFound();
//             }
//             return View(joke);
//         }

//         // POST: Jokes/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
//         {
//             if (id != joke.Id)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(joke);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!JokeExists(joke.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(joke);
//         }

//         // GET: Jokes/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null || _context.Joke == null)
//             {
//                 return NotFound();
//             }

//             var joke = await _context.Joke
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (joke == null)
//             {
//                 return NotFound();
//             }

//             return View(joke);
//         }

//         // POST: Jokes/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             if (_context.Joke == null)
//             {
//                 return Problem("Entity set 'ApplicationDbContext.Joke'  is null.");
//             }
//             var joke = await _context.Joke.FindAsync(id);
//             if (joke != null)
//             {
//                 _context.Joke.Remove(joke);
//             }
            
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool JokeExists(int id)
//         {
//           return (_context.Joke?.Any(e => e.Id == id)).GetValueOrDefault();
//         }
//     }
// }

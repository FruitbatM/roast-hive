using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoastHiveMvc.Models;
using RoastHiveMvc.Data;


namespace RoastHiveMvc.Controllers;

public class DummyController : Controller
{
    private readonly ProductsDbContext _db;
    public DummyController(ProductsDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> objProductList = _db.Product;
        return View(objProductList);
    }
    //Create GET
    public IActionResult Create()
    {
       return View();
    }

    //Create POST
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public IActionResult Create(Product obj)
    {
        //Validations
        if(ModelState.IsValid){
            _db.Product.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
       return View(obj);
    }

    //Edit GET
    public IActionResult Edit(int? id)
    {
       if(id==null || id == 0){
            return NotFound();
        }
        var productFromDb = _db.Product.Find(id);
        //var productFromDbFirst = _db.Product.FirstOrDefault(u => u.ProdID==id);
        //var productFromDbSingle = _db.Product.SingleOrDefault(u => u.ProdID==id);
        
        if(productFromDb == null){
            return NotFound();
        }

        return View(productFromDb);
    }

    //Edit POST
    [HttpPost]
    public IActionResult Edit(Product obj)
    {
        if(ModelState.IsValid){
            _db.Product.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
       return View(obj);
    }


    //Delete GET
    public IActionResult Delete(int? id)
    {
       if(id==null || id == 0){
            return NotFound();
        }
        var productFromDb = _db.Product.Find(id);
        
        if(productFromDb == null){
            return NotFound();
        }

        return View(productFromDb);
    }

    //Delete POST
    [HttpPost]
    public IActionResult DeletePOST(int? id)
    {
        
        var obj = _db.Product.Find(id);
       
        if(obj == null){
            return NotFound();
        }

        _db.Product.Remove(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

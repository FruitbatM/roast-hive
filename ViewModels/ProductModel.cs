using RoastHiveMvc.Models;
public class ProductModel 
{
    public List<Product> _products { get; set; }
    public List<Product> findAll()
    {
        return _products;
    }
    public Product find(string id)
    {
        List<Product> products = findAll();
        var prod = products.Where(a => a.Id == id).FirstOrDefault();
            return prod;     
    }
}
using RoastHiveMvc.Models;

namespace RoastHiveMvc.ViewModels 
{
    public class ProductVM
    {
        public List<Product>? CoffeeProducts { get; set; }
        public List<Product>? EquipmentProducts { get; set; }
    }
}
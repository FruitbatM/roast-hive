using System.ComponentModel.DataAnnotations;
using RoastHiveMvc.Models;

namespace RoastHiveMvc.Models
{
    public class Item
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
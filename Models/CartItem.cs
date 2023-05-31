using System.ComponentModel.DataAnnotations;

namespace RoastHiveMvc.Models
{
    public class CartItem
    {
        public int CartId { get; set; } //specifies the ID of the user that is associated with the item to purchase
        public int ProdID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice {get; set;}
        public System.DateTime DateCreated { get; set; }

        // public int ProductId { get; set; }

        // public virtual Product Product { get; set; }

        // public static implicit operator CartItem(CartItem v)
        // {
        //     throw new NotImplementedException();
    }
}

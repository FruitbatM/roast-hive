using System.ComponentModel.DataAnnotations;

namespace RoastHiveMvc.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public List<CartItem> Items { get; set; }

        public decimal Total {
            get{
                return (decimal)Items.Sum(item => item.UnitPrice * item.Quantity);
            }
        }

        public DateTime DateCreated { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
        }

        public void Add(CartItem item)
        {
            Items.Add(item);
        }

        public void Remove(int itemId)
        {
            // Remove all items that match the product ID from the items currently in the cart.
            Items.RemoveAll(x => x.ProductId == itemId);
        }

        internal dynamic Sum(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
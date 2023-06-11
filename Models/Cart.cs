using System;
using System.Collections.Generic;
using System.Linq;

namespace RoastHiveMvc.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public List<CartItem> Items { get; set; }

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
            Items.RemoveAll(x => x.ProductId == itemId);
        }

        public void UpdateQuantity(int itemId, int quantity)
        {
            var item = Items.FirstOrDefault(x => x.ProductId == itemId);
            if (item != null)
            {
                item.Quantity = quantity;
                item.Total = (decimal)(item.UnitPrice * item.Quantity);
            }
        }

        public decimal CalculateTotal()
        {
            double total = Items.Sum(item => item.UnitPrice * item.Quantity);
            return (decimal)total;
        }
        public decimal Total => CalculateTotal();

    }
}
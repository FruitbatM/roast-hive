using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using RoastHiveMvc.Models;

namespace RoastHiveMvc.Models
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class Item
    {
        private Product? product;

        public Product Product { get => product; set => product = value; }
        public int Quantity { get; set; }

        private string GetDebuggerDisplay() => ToString();
    }
}
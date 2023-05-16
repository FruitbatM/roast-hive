using Microsoft.EntityFrameworkCore;
using RoastHiveMvc.Models;
namespace RoastHiveMvc.Data
{
   public class ProductsDbContext : DbContext
   {
       public ProductsDbContext(DbContextOptions<ProductsDbContext> options) :
base(options)
        {
            
        }
       public DbSet<Product> Products { get; set; } = null!;
   }
}
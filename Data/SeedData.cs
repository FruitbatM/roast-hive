using Microsoft.EntityFrameworkCore;
using RoastHiveMvc.Models;

namespace RoastHiveMvc.Data;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ProductsDbContext(serviceProvider.GetRequiredService<DbContextOptions<ProductsDbContext>>()))
       {        
        if (context.Products.Any())
            {
            return; 
            }
        context.Products.AddRange(
            new Product {Name = "Ethiopia Yirgacheffe", Description = "Organic Worka Chelbessa. Whole Bean. Taste Notes: Peach, Boysenberry, Elderflower", Size = "340g", UnitPrice = 23.99, Origin = "Ethiopia"});
        context.SaveChanges();
        }
    }
        
} 
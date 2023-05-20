using Microsoft.EntityFrameworkCore;
using RoastHiveMvc.Models;

namespace RoastHiveMvc.Data;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ProductsDbContext(serviceProvider.GetRequiredService<DbContextOptions<ProductsDbContext>>()))
       {        
        if (context.Product.Any())
            {
            return; 
            }
        context.Product.AddRange(
            new Product {CatId = "Single Origin", Name = "Ethiopia Yirgacheffe", Description = "Organic Worka Chelbessa. Whole Bean. Taste Notes: Peach, Boysenberry, Elderflower", Size = "340g", UnitPrice = 23.99, Origin = "Ethiopia"},
            new Product {CatId = "Single Origin", Name = "Colombia Supremo", Description = "Flavourful, medium-bodied coffee with a sweet, nut-like aroma and a smooth taste", Size = "220g", UnitPrice = 10.00, Origin = "Colombia"},
            new Product {CatId = "Single Origin", Name = "Costa Rica Tarrazu", Description = "Light roast, Sweet and balanced, with a creamy body, bright acidity and notes of milk chocolate and citrus", Size = "1lb", UnitPrice = 19.99, Origin = "Costa Rica"},          
            new Product {CatId = "Single Origin", Name = "Kenya AA", Description = "Complex, fruity, light, and very bright cup. Directly sourced. Expertly roasted for optimal quality and taste", Size = "1lb", UnitPrice = 25.99, Origin = "Kenya"},
            new Product {CatId = "Single Origin", Name = "Brazili Santos", Description = "Soft, smooth and creamy mouthfeel with notes of chocolate, vanilla and a sweet clean caramel finish. Suitable for filter brew method and a mild strength espresso", Size = "500g", UnitPrice = 25.00, Origin = "Brazil"}  
             );
        context.SaveChanges();
        }
    }
        
} 
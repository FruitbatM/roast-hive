using Microsoft.EntityFrameworkCore;
using RoastHiveMvc.Models;

namespace RoastHiveMvc.Data;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ProductsDbContext(serviceProvider.GetRequiredService<DbContextOptions<ProductsDbContext>>()))
       {        
        /*if (context.Product.Any())
            {
            return; 
            }*/
        context.Product.AddRange(
            new Product {Name = "Ethiopia Yirgacheffe", Description = "Organic Worka Chelbessa. Whole Bean. Taste Notes: Peach, Boysenberry, Elderflower", Size = "340g", UnitPrice = 23.99, Origin = "Ethiopia"},
            new Product {Name = "Colombia Supremo", Description = "Flavourful, medium-bodied coffee with a sweet, nut-like aroma and a smooth taste", Size = "220g", UnitPrice = 10, Origin = "Colombia"},
            new Product {Name = "Costa Rica Tarrazu", Description = "Light roast, Sweet and balanced, with a creamy body, bright acidity and notes of milk chocolate and citrus", Size = "1lb", UnitPrice = 19.99, Origin = "Costa Rica"},
            new Product {Name = "Kenya AA", Description = "Complex, fruity, light, and very bright cup. Directly sourced. Expertly roasted for optimal quality and taste", Size = "1lb", UnitPrice = 25.99, Origin = "Kenya"},
            new Product {Name = "Brazili Santos", Description = "Soft, smooth and creamy mouthfeel with notes of chocolate, vanilla and a sweet clean caramel finish. Suitable for filter brew method and a mild strength espresso", Size = "500g", UnitPrice = 25, Origin = "Brazil"},
            new Product {Name = "Guatemala Antigua", Description = "100% Arabica, the perfect mix of sweetness and acidity, a hint of cocoa with a spicy aroma.", Size = "500g", UnitPrice = 11.99, Origin = "Guatemala"},
            new Product {Name = "Mocha Java Blend", Description = "Medium Roast. Almond, Blueberry and Chocolate Notes", Size = "310g", UnitPrice = 20.99, Origin = "Java"},
            new Product {Name = "Breakfast Blend", Description = "Strong, smooth and rich in taste, is a delicious choice for preparing espresso and cappuccino", Size = "340g", UnitPrice = 23.99, Origin = "South America"},
            new Product {Name = "French Roast Blend", Description = "Don Francisco's Double French Dark Roast Ground Coffee", Size = "10z", UnitPrice = 29.99, Origin = "Cuba"},
            new Product {Name = "Italian roast Blend", Description = "Union Hand Roasted Coffee - Daily Roast Coffee Beans - Dark Roast", Size = "1kg", UnitPrice = 27.5, Origin = "Ethiopia"},
            new Product {Name = "Colombian Blend", Description = "Colombian Supremo Coffee", Size = "16oz", UnitPrice = 25.99, Origin = "Colombia"},
            new Product {Name = "City Blend", Description = "Santos City Blend. This is a four origin blend roasted medium-dark, giving rich, warm, chocolate flavour notes. Medium acidity.", Size = "250g", UnitPrice = 18, Origin = "New Zealand"},
            new Product {Name = "NYC Blend", Description = "New York City Blend - Unique Coffee Roasters.", Size = "1lb", UnitPrice = 20, Origin = "Costa Rica"},
            new Product {Name = "Lavazza Super Crema Espresso", Description = "Delicate espresso with a long-lasting cream and a pleasantly chocolatey finish", Size = "1kg", UnitPrice = 49.99, Origin = "Italy"},
            new Product {Name = "Counter Culture Espresso Hologram", Description = "Medium Roast. Notes of ripe fruit and chocolate", Size = "12oz", UnitPrice = 3.75, Origin = "Ethiopia"},
            new Product {Name = "Stumptown Hair Bender Espresso", Description = "Hair Bender relies on coffees from Latin America, Indonesia, and Africa to achieve a complex flavour profile with notes of sweet citrus, dark chocolate, and raisin.", Size = "12oz", UnitPrice = 20, Origin = "Latin America"},
            new Product {Name = "Blue Bottle Bella Donovan Espresso", Description = "Bella Donovan is made up of organic coffee beans. A natural Ethiopian coffee with jammy berry notes brings a glimmer of fruit to the otherwise chocolate-heavy foundation", Size = "12oz", UnitPrice = 15, Origin = "Ethiopia"},
            new Product {Name = "Cinnamon Swirl Decaf", Description = "This indulgent Cinnamon Swirl blend offers the decadent flavor of cinnamon you love without the caffeine that keeps you awake", Size = "1lb", UnitPrice = 23, Origin = "Colombia"},
            new Product {Name = "Sumatra - Decaf", Description = "Starbucks Sumatra Decaf. Strikingly bold and full-bodied with rich herbals, rustic spice notes and a muted acidity", Size = "1lb", UnitPrice = 25.99, Origin = "Indonesia"},
            new Product {Name = "French Roast Decaf", Description = "Peets Coffee Decaf French Roast. Notes of Smoke, Dark Chocolate, Burnt Sugar", Size = "454g", UnitPrice = 15.99, Origin = "Americas"},
            new Product {Name = "Swiss Water Process Decaf", Description = "Swiss Water Process Decaf. Citrus, Chocolate, Juicy tones", Size = "12oz", UnitPrice = 19.99, Origin = "Matagalpa"},
            new Product {Name = "Caribou Coffee Decaf Caribou Blend", Description = "Caribou Blend Decaf. Medium roast. Woodsy, spicy notes are balanced with a touch of sweetness and bright acidity", Size = "1lb", UnitPrice = 14.99, Origin = "Indonesia"},
            new Product {Name = "Specialty Drip Coffee Maker", Description = "Stainless Steel, 45 cup Coffee Maker, 4 strength settings. built in wifi connect", Size = "", UnitPrice = 350, Origin = ""},
            new Product {Name = "BELLISSIMO Semi Automatic Espresso Machine + Frother", Description = "Free Standng, 15 Bar, 12V, 2.8 L White Espresso Machine with 16 settings, built in wifi connect", Size = "", UnitPrice = 500, Origin = ""},
            new Product {Name = "AFFETTO Automatic Espresso Machine", Description = "Free Standng, 20 Bar, 12V, 1.2 L White Espresso Machine with 5 settings, built in wifi connect", Size = "", UnitPrice = 500, Origin = ""},
            new Product {Name = "Cold Brew Coffee Maker", Description = "OXO Brew Compact Metallic Coffee Maker One Size, 700ml, free standing", Size = "", UnitPrice = 90, Origin = ""});
        context.SaveChanges();
        }
    }
        
} 
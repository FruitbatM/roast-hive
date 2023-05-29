using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RoastHiveMvc.Models;

namespace RoastHiveMvc.Data
{
    public class RoastHiveDbContext : IdentityDbContext
    {
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
    }

        internal static IEnumerable<object> ToList()
        {
            throw new NotImplementedException();
        }

        public RoastHiveDbContext(DbContextOptions<RoastHiveDbContext> options) :
    base(options)
    {}

        public RoastHiveDbContext()
        {
        }

        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<CartItem> ShoppingCartItems { get; set; } = null!;
    }
}

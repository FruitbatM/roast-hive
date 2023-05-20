using Microsoft.EntityFrameworkCore;
using RoastHiveMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RoastHiveMvc.Data
{
    public class ProductsDbContext : IdentityDbContext
    {
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) :
    base(options)
    {}
    public DbSet<Product> Product { get; set; } = null!;
    }
}

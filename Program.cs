using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoastHiveMvc.Models;
using RoastHiveMvc.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration
            .GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string'DefaultConnection' not found.");

        builder.Services.AddDbContext<ProductsDbContext>(options =>
            options.UseSqlite(connectionString));

        builder.Services.AddControllersWithViews();

        var app = builder.Build();
        // using (var scope = app.Services.CreateScope())
        // {
        //     var services = scope.ServiceProvider;
        //     SeedData.Initialize(services);
        // }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
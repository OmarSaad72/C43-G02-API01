using Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbInitializer(AppDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task InitializeAsync()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    await _dbContext.Database.MigrateAsync();
                    if (!_dbContext.ProductTypes.Any())
                    {
                        //C:\Users\OmarSaad\source\repos\E-CommerceProject\Infrastructure\Persistance\Data\DataSeeding\types.json
                        var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\types.json");
                        var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                        if (types != null && types.Any())
                        {
                            await _dbContext.AddRangeAsync(types);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    if (!_dbContext.ProductBrands.Any())
                    {
                        //C:\Users\OmarSaad\source\repos\E-CommerceProject\Infrastructure\Persistance\Data\DataSeeding\brands.json
                        var BrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\brands.json");
                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                        if (brands != null && brands.Any())
                        {
                            await _dbContext.AddRangeAsync(brands);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    if (!_dbContext.Products.Any())
                    {
                        //C:\Users\OmarSaad\source\repos\E-CommerceProject\Infrastructure\Persistance\Data\DataSeeding\products.json
                        var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\products.json");
                        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                        if (products != null && products.Any())
                        {
                            await _dbContext.AddRangeAsync(products);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public async Task InitializeIdentityAsync()
        {
            // Seed Roles
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            //Seed Users
            if (!_userManager.Users.Any())
            {
                var adminUser = new User()
                {
                    DisplayName = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "1234567890",
                };
                var superAdminUser = new User()
                {
                    DisplayName = "SuperAdmin",
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "474839373",
                };
                await _userManager.CreateAsync(adminUser, "P@ssW0rd");
                await _userManager.CreateAsync(superAdminUser, "P@ssW0rd");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
            }
        }
    }
}

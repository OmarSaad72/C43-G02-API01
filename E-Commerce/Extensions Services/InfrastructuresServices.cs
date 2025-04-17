using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.DataSeeding;
using Persistance.Identity;
using Persistance.Repositories;
using Persistence.Data;
using StackExchange.Redis;

namespace E_Commerce.Extensions
{
    public static class InfrastructuresServices
    {
        public static IServiceCollection AddInfrastructuresServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(options =>  // LifeTime of the DbContext is Scoped
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));  // DB for Product
            });
            services.AddDbContext<IdentityAppDbContext>(options =>  // LifeTime of the DbContext is Scoped
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));  // DB for Product
            });
            services.AddSingleton<IConnectionMultiplexer>(services => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!)); // DB for Basket
            services.AddScoped<IBasketRepo, BasketRepo>();
            services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireNonAlphanumeric = true;
                //o.Password.RequireDigit = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireLowercase = true;
                o.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityAppDbContext>();
            return services;
        }
    }
}

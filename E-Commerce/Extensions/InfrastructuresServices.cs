using Domain.Contracts;
using E_Commerce.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.DataSeeding;
using Persistance.Repositories;
using Persistence.Data;

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
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}

using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance.Data;
using Persistance.Identity;
using Persistance.Repositories;
using Persistence.Data;
using Shared;
using StackExchange.Redis;
using System.Text;

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
            services.ConfigureJwt(configuration);
            return services;
        }
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            // Validate Token
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });
            services.AddAuthorization();
            return services;
        }
    }
}

using Services.Abstraction;
using Services;
using Shared;

namespace E_Commerce.Extensions
{
    public static class CoreServicers
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration) //IServiceCollection ==> builder.Services{Extension Method} 
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);  // LifeTime: Transient
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}

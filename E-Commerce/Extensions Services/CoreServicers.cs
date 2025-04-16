using Services.Abstraction;
using Services;

namespace E_Commerce.Extensions
{
    public static class CoreServicers
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services) //IServiceCollection ==> builder.Services{Extension Method} 
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);  // LifeTime: Transient
            return services;
        }
    }
}

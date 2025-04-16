using Domain.Contracts;
using E_Commerce.Middlewares;
using System.Reflection.Metadata.Ecma335;

namespace E_Commerce.Extensions
{
    public static class WebAppExtensions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            return app;
        }

        public static WebApplication UseCustomMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}

using E_Commerce.Extensions;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Services
            var builder = WebApplication.CreateBuilder(args);

            // Add Infrastructures Services
            builder.Services.AddInfrastructuresServices(builder.Configuration);

            //Add Core service
            builder.Services.AddCoreServices();

            // Add Presentation Services
            builder.Services.AddPresentationServices();
            #endregion

            #region PipleLines{Middlewares}
            var app = builder.Build();

            await app.SeedDbAsync();
            app.UseCustomMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}

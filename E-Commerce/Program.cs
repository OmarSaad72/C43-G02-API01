using Domain.Contracts;
using E_Commerce.Extensions;
using E_Commerce.Factories;
using E_Commerce.Middlewares;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.DataSeeding;
using Persistance.Repositories;
using Persistence.Data;
using Services;
using Services.Abstraction;
using StackExchange.Redis;

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

            app.UseCustomMiddleware();
            await app.SeedDbAsync();

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

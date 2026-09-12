using System.Text.Json.Serialization;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using persistence.Data;
using persistence.Repositories;
using Services;
using Services.Abstraction.Contracts;
using Services.Immplemntations;
using System.Reflection.Metadata;
using E_Commerce.Api.Middleware;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Api.Factories;
using E_Commerce.Api.Extensions;

namespace E_Commerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            #region DI Container
            var builder = WebApplication.CreateBuilder(args);

            // Add Web Api services

            builder.Services.AddWebApiServices(builder.Configuration);

            // Add Infrastruce Services
            builder.Services.AddInfrastruceServices(builder.Configuration);

            // Add Core Services
            builder.Services.AddCoreServices(builder.Configuration);
            #endregion

            #region Middle Wares
            var app = builder.Build();
            await app.SeedDataAsync();



            await app.UseExceptionHandlingMiddleWares();
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
               await app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
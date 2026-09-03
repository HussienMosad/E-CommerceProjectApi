
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using persistence.Data;
using persistence.Repositories;
using Services;
using Services.Abstraction.Contracts;
using Services.Immplemntations;
using System.Reflection.Metadata;

namespace E_Commerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Access DbContext
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDataSeeding , DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
            builder.Services.AddAutoMapper(cfg => { }, typeof(AssemblyRefrence).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
           await  ObjectOfDataSeeding.SeedDataAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
using Domain.Contracts;
using E_Commerce.Api.Middleware;

namespace E_Commerce.Api.Extensions
{
    public static class WebApplicationsExtensions
    {

        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.SeedDataAsync();
            await ObjectOfDataSeeding.SeedIdentityDataAsync();
            return app;
        }

        public static async Task<WebApplication> UseExceptionHandlingMiddleWares(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return app;
        }

        public static async Task<WebApplication> UseSwaggerMiddleWares(this WebApplication app)
        {
            app.MapOpenApi();

            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
using E_Commerce.Api.Factories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace E_Commerce.Api.Extensions
{
    public static class WebApiServicesExtension
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection Services)
        {
           Services
               .AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(
                       new JsonStringEnumConverter());
               });

            Services.AddOpenApi();
            Services.AddSwaggerGen();
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponceFactory.CustomValidationErrorResponse;
            });

            return Services;
        }
    }
}
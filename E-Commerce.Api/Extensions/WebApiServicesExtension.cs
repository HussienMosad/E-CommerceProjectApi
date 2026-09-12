using E_Commerce.Api.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using System.Text.Json.Serialization;


namespace E_Commerce.Api.Extensions
{
    public static class WebApiServicesExtension
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection Services , IConfiguration configuration)
        {
           Services
               .AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(
                       new JsonStringEnumConverter());
               });
            var frontUrl = configuration.GetSection("URLs")["FrontUrl"];

            Services.AddCors(options =>
            {
                // URL => URL PROJECT ANGULAR [CLIENT]
                // HEADER, METHODS [GET, POST]
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithOrigins(frontUrl);
                });
            });

            Services.AddOpenApi();
            Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Demo API",
                        Version = "v1"
                    });

                option.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });

                option.AddSecurityRequirement(document =>
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecuritySchemeReference(
                                "Bearer",
                                document)
                        ] = new List<string>()
                    });
            });
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponceFactory.CustomValidationErrorResponse;
            });

            return Services;
        }
    }
}
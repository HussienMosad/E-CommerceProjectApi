using Microsoft.AspNetCore.Authentication;
using Services;
using Services.Abstraction.Contracts;
using Services.Immplemntations;
using Shared.Common;
using Stripe.Climate;
using System.Reflection.Metadata;
using ProductService = Services.Immplemntations.ProductService;
using AuthenticationService = Services.Immplemntations.AuthenticationService;
using IAuthenticationService = Services.Abstraction.Contracts.IAuthenticationServices;

namespace E_Commerce.Api.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services , IConfiguration configuration) 
        {

           services.AddAutoMapper(cfg => { }, typeof(AssemblyRefrence).Assembly);
            
            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IBasketService, BasketService>();

            services.AddScoped<Func<IProductService>>(provider =>
                () => provider.GetRequiredService<IProductService>()
            );

            services.AddScoped<Func<IAuthenticationService>>(provider =>
                () => provider.GetRequiredService<IAuthenticationService>()
            );

            services.AddScoped<Func<IBasketService>>(provider =>
                () => provider.GetRequiredService<IBasketService>()
            );

            services.AddScoped<Func<IOrderServices>>(provider =>
                () => provider.GetRequiredService<IOrderServices>()
            );

            services.AddScoped<Func<IPaymentServices>>(provider =>
                () => provider.GetRequiredService<IPaymentServices>()
            );


            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}

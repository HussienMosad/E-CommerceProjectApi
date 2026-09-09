using Services;
using Services.Abstraction.Contracts;
using Services.Immplemntations;
using Shared.Common;

namespace E_Commerce.Api.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services , IConfiguration configuration) 
        {

           Services.AddAutoMapper(cfg => { }, typeof(AssemblyRefrence).Assembly);
           Services.AddScoped<IServiceManager, ServiceManager>();
            Services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return Services;
        }
    }
}

using Services;
using Services.Abstraction.Contracts;
using Services.Immplemntations;

namespace E_Commerce.Api.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services) 
        {

           Services.AddAutoMapper(cfg => { }, typeof(AssemblyRefrence).Assembly);
           Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}

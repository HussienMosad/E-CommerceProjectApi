using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using persistence.Data;
using persistence.Repositories;
using System.Runtime.CompilerServices;

namespace E_Commerce.Api.Extensions
{
    public static class InfrastructeServicesExtension
    {
        public static IServiceCollection AddInfrastruceServices(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            return Services;
        }
    }
}

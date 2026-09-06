using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using persistence.Data;
using persistence.Repositories;
using StackExchange.Redis;
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

            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
               return  ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnection")!);
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository , BasketRepository>();
            return Services;
        }
    }
}

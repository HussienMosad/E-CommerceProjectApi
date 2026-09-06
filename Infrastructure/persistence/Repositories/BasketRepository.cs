using Domain.Contracts;
using Domain.Entities.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var result = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            return result ? await GetBasketByIdAsync(basket.Id) : null;
        }

        public async Task<bool> DeleteBasket(string id)
        => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetBasketByIdAsync(string id)
        {
            var result = await _database.StringGetAsync(id);
            if(result.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<CustomerBasket>(result.ToString());
        }
    }
}

using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer _connectionMultiplexer) : ICacheRepository
    {
        private readonly IDatabase _database = _connectionMultiplexer.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
           var result = await _database.StringGetAsync(key);
           return result.IsNullOrEmpty ? default : result;
        }

        public async Task SetAsync(string key, object Value, TimeSpan duration)
        {
            var objSerilzed = JsonSerializer.Serialize(Value);
             await _database.StringSetAsync(key, objSerilzed, duration);
        }
    }
}

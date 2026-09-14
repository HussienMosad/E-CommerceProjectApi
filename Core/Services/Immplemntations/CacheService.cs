using Domain.Contracts;
using Services.Abstraction.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Immplemntations
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public Task<string?> GetCachedDataAsync(string cacheKey)
         => _cacheRepository.GetAsync(cacheKey);

        public Task SetCacheDataAsync(string cacheKey, object value, TimeSpan duration)
        => _cacheRepository.SetAsync(cacheKey, value, duration);
    }
}

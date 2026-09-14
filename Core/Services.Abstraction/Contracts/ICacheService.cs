using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction.Contracts
{
    public interface ICacheService
    {
        Task<string?> GetCachedDataAsync(string cacheKey);

        Task SetCacheDataAsync(string cacheKey, object value , TimeSpan duration);
    }
}

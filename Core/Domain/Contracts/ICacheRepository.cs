using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string key);

        Task SetAsync(string key, object Value , TimeSpan duration);
    }
}

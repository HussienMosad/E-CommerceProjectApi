using Domain.Entities.BasketModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketByIdAsync(string id);


        Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket basket , TimeSpan? TimeToLive = null);

        Task<bool> DeleteBasket(string id);
    }
}

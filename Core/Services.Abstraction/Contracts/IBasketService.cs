using Shared.Dtos.BasketDtos;

namespace Services.Abstraction.Contracts
{
    public interface IBasketService
    {
        // Get Basket
        Task<BasketDto> GetBasketAsync(string Id);
        // Delete Basket
        Task<bool> DeleteBasketAsync(string Id);
        // Create Or Update Basket
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto BasketDto);
    }
}

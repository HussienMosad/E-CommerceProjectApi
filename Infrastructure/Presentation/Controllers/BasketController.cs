using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Controllers
{
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {

        // Get => Get Basket
        // BaseUrl/Basket/BaketId
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string Id)
        => Ok(await _serviceManager.BasketService.GetBasketAsync(Id));

        // Post => Create Basket
       // BaseUrl/Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreaeOrUpdateBasketAsync(BasketDto basket)
            => Ok(await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket));

        // Delete => DeleteBasket
        // BaseUrl / Basket/BasketId
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteBasketAsync(string Id)
        {
           await _serviceManager.BasketService.DeleteBasketAsync(Id);
            return NoContent(); // 204
        }

    }
}

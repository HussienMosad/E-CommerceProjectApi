using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Immplemntations
{
    public class BasketService(IBasketRepository _repository , IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto BasketDto)
        {
            var Basket = _mapper.Map<CustomerBasket>(BasketDto);
            var CreatedOrUpdatedBasket = await _repository.CreateOrUpdateBasket(Basket);

            return CreatedOrUpdatedBasket is null ? throw new Exception("Failed To Create Or Update This Basket")
                : _mapper.Map<BasketDto>(CreatedOrUpdatedBasket);
        }

        public async Task<bool> DeleteBasketAsync(string Id)
       => await _repository.DeleteBasket(Id);

        public async Task<BasketDto> GetBasketAsync(string Id)
        {
            var Basket = await _repository.GetBasketByIdAsync(Id);
            
            return Basket is null ? throw new BasketNotFoundException(Id) 
                :_mapper.Map<BasketDto>(Basket);
        }
    }
}

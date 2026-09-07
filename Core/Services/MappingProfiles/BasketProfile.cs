using AutoMapper;
using Domain.Entities.BasketModule;
using Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}

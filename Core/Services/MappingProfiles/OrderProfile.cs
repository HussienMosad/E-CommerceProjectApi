using AutoMapper;
using Domain.Entities.OrderModule;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {

        public OrderProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodResult>();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, Options => Options.MapFrom(src => src.ProductInOrderItem.ProductId))
                .ForMember(dest => dest.ProductName, Options => Options.MapFrom(src => src.ProductInOrderItem.ProductName))
                .ForMember(dest => dest.PictureUrl, Options => Options.MapFrom(src => src.ProductInOrderItem.PictureUrl));

            CreateMap<Order, OrderResult>()
                .ForMember(dest => dest.PaymentStatus, Options => Options.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.DeliveryMethod, Options => Options.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, Options => Options.MapFrom(src => src.SubTotal + src.DeliveryMethod.Price));
                
         }
    }
}

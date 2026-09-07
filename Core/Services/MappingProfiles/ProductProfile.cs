using AutoMapper;
using Domain.Entities.ProductModule;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.MappingProfiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand ,BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();
            CreateMap<Product, ProductResultDto>()
                        .ForMember(dest => dest.BrandName, Opt => Opt.MapFrom(src => src.ProductBrand.Name))
                        .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                        .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());
        }
    }
}

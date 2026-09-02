using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Services.Abstraction.Contracts;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Immplemntations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork UnitOfWork , IMapper Mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = Mapper;
        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var Brands =await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

           return _mapper.Map<IEnumerable<BrandResultDto>>(Brands);

        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProuductsAsync()
        {
            var Product = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResultDto>>(Product);
          
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeResultDto>>(Types);
     
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var Product = await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(id);
            if (Product is null) return null;

         return _mapper.Map<ProductResultDto>(Product);
        }
    }
}
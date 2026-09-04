using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared;
using Shared.Dtos;
using Shared.Enums;
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

        public async Task<PaginatedResult<ProductResultDto>> GetAllProuductsAsync(ProductSpacificationParameters parameters)
        {
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            var Specifications = new ProductWithBrandAndTypeSpecifications(parameters);
            var Product = await ProductRepo.GetAllAsync(Specifications);
            var ProductResult = _mapper.Map<IEnumerable<ProductResultDto>>(Product);


            var PageSize = ProductResult.Count();
            var CountSpecifications = new ProductCountSpecifications(parameters);
            var TotalCount = await ProductRepo.CountAsync(CountSpecifications);

            return new PaginatedResult<ProductResultDto>(parameters.PageIndex, PageSize, TotalCount , ProductResult);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeResultDto>>(Types);
     
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(Specifications);
            if (Product is null) return null;

         return _mapper.Map<ProductResultDto>(Product);
        }
    }
}
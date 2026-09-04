using Shared;
using Shared.Dtos;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction.Contracts
{
   public interface IProductService
    {
        //Get All Products 
        Task<PaginatedResult<ProductResultDto>> GetAllProuductsAsync(ProductSpacificationParameters parameters);

        //Get All Brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        //Get All Types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

        //Get Product By Id

        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}

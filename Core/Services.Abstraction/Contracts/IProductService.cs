using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction.Contracts
{
   public interface IProductService
    {
        //Get All Products 
        Task<IEnumerable<ProductResultDto>> GetAllProuductsAsync();

        //Get All Brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        //Get All Types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

        //Get Product By Id

        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}

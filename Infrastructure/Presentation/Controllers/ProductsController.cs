using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos;
using Shared.Enums;
using System.Net;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager ) :ControllerBase
    {
        //End Point ==>  Get All Product
        // BaseUrl / Products
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProductsAsync([FromQuery] ProductSpacificationParameters parameters)
        => Ok(await _serviceManager.ProductService.GetAllProuductsAsync(parameters));



        //Get All Types
        // BaseUrl /Products/Types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            => Ok(await _serviceManager.ProductService.GetAllTypesAsync());



        //End Point ==>  Get All Brands
        // BaseUrl / Products / Brands

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());



        //End Point ==>  Get Product By ID 
        // BaseUrl / Products / 10

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));
    }
}

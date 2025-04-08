using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.DTOs;

namespace Presentaion
{
    [ApiController]
    [Route("api/[controller]"/*==>Variable Segment*/)]  //baseUrl/api/productcontroller
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDTO>>> GetAllProductAsync([FromQuery]ProductParametersSpecifications productParametersSpecifications)
        {
            var Products = await serviceManager.ProductService.GetAllProductsAsync(productParametersSpecifications);
            return Ok(Products);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDTO>>> GetAllProductBrandsAsync()
        {
            var Products = await serviceManager.ProductService.GetAllProductsBrandAsync();
            return Ok(Products);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDTO>>> GetAllProductTypesAsync()
        {
            var Products = await serviceManager.ProductService.GetAllProductsTypeAsync();
            return Ok(Products);
        }

        [HttpGet("Id")]
        public async Task<ActionResult<ProductResultDTO>> GetProductByIdAsync(int id)
        {
            var Products = await serviceManager.ProductService.GetProductsByIdAsync(id);
            return Ok(Products);
        }
    }

}

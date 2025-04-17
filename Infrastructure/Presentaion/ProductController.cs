using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.DTOs;
using Shared.ErrorModels;
using System.Net;

namespace Presentation
{
     //baseUrl/api/productController
    public class ProductsController(IServiceManager serviceManager) : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDTO>>> GetAllProductAsync([FromQuery]ProductSpecificationsParameters productParametersSpecifications)
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
       
        [ProducesResponseType(typeof(ProductResultDTO), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDTO>> GetProductByIdAsync(int id)
        {
            var Products = await serviceManager.ProductService.GetProductsByIdAsync(id);
            return Ok(Products);
        }
    }

}

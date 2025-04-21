global using Shared;
using Shared.DTOs;
namespace Services.Abstraction
{
    public interface IProductService
    {
        // Get All Products:
        public Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationsParameters productParametersSpecifications);
        // Get All ProductsBrand:
        public Task<IEnumerable<BrandResultDTO>> GetAllProductsBrandAsync();
        // Get All ProductsType:
        public Task<IEnumerable<TypeResultDTO>> GetAllProductsTypeAsync();
        // Get Product By Id:
        public Task<ProductResultDTO?> GetProductsByIdAsync(int Id);
    }
}

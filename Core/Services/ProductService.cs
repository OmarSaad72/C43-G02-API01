using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using Shared.DTOs;
namespace Services
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductParametersSpecifications parameters)
        {
            // 1.Retrieve All Product ==> Calling UnitOfWork
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(parameters));
            var totalCount = await unitOfWork.GetRepository<Product, int>().TotalCountAsync(new ProductCountSpecifications(parameters));
            // 2.Map To ProductDTO ==> Using AutoMapper
            var productResult = mapper.Map<IEnumerable<ProductResultDTO>>(products);
            // 3.Return
            //return result;
            var result = new PaginatedResult<ProductResultDTO>
                (
                productResult.Count(),
                parameters.PageIndex,
                totalCount,
                productResult
                );
            return result;
        }

        public async Task<IEnumerable<BrandResultDTO>> GetAllProductsBrandAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<BrandResultDTO>>(brands);
            return result;
        }

        public async Task<IEnumerable<TypeResultDTO>> GetAllProductsTypeAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeResultDTO>>(types);
            return result;
        }

        public async Task<ProductResultDTO?> GetProductsByIdAsync(int Id)
        {
            var productId = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(Id));
            var result = mapper.Map<ProductResultDTO>(productId);
            return result;
        }
    }
}

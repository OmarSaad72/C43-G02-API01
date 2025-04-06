using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Services.Specifications;
using Shared;
namespace Services
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync()
        {
            // 1.Retrieve All Product ==> Calling UnitOfWork
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications());
            // 2.Map To ProductDTO ==> Using AutoMapper
            var result = mapper.Map<IEnumerable<ProductResultDTO>>(products);
            // 3.Return
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

using Domain.Contracts;
using Domain.Entities;
using Shared;
using System.Reflection.Metadata;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        // Retrieve Products By Id (Where[Id], Include[Brand, Type])
        public ProductWithBrandAndTypeSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        // Retrieve All Products (Include[Brand, Type])
        public ProductWithBrandAndTypeSpecifications(ProductParametersSpecifications Parameters)
            : base(p =>
            (!Parameters.BrandId.HasValue || p.BrandId == Parameters.BrandId) && 
            (!Parameters.TypeId.HasValue || p.TypeId == Parameters.TypeId))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            if (Parameters.Sort != null)
            {
                switch (Parameters.Sort)
                {
                    case ProductSortOptions.PriceDesc:
                        SetOrderByDesc(p => p.Price);
                        break;
                    case ProductSortOptions.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case ProductSortOptions.NameDesc:
                        SetOrderByDesc(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }
            ApplyPagination(Parameters.PageIndex, Parameters.PageSize);
        }
    }
}

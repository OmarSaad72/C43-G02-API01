using Domain.Contracts;
using Domain.Entities;
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

        //ApplyPagination(Specifications.ProductWithBrandAndTypeSpecifications pagination);

        // Retrieve All Products (Include[Brand, Type])
        public ProductWithBrandAndTypeSpecifications(string? sort, int? brandId, int? typeId)
            : base(p =>
            (!brandId.HasValue || p.BrandId == brandId) && 
            (!typeId.HasValue || p.TypeId == typeId))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort.ToLower().Trim())
                {
                    case "pricedesc":
                        SetOrderByDesc(p => p.Price);
                        break;
                    case "pricedasc":
                        SetOrderBy(p => p.Price);
                        break;
                    case "namedesc":
                        SetOrderByDesc(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }
        }
    }
}

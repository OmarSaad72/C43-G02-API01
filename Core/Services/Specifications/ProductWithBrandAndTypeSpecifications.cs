using Domain.Contracts;
using Domain.Entities;

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
        public ProductWithBrandAndTypeSpecifications() : base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}

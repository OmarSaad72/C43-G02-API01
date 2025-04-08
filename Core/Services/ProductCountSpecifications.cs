using Domain.Contracts;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductCountSpecifications : Specifications<Product>
    {
        public ProductCountSpecifications(ProductParametersSpecifications Parameters)
            : base(p =>
            (!Parameters.BrandId.HasValue || p.BrandId == Parameters.BrandId) &&
            (!Parameters.TypeId.HasValue || p.TypeId == Parameters.TypeId))
        {
        }

    }
}

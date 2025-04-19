using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithIncludesSpecifications : Specifications<Order>
    {
        // Retrieve Order By Id (Where[Id], Include[OrderItems, DeliveryMethods]) ==> Return One Object
        public OrderWithIncludesSpecifications(Guid id) : base(x => x.Id == id)
        {
            AddInclude(o => o.DeliveryMethods);
            AddInclude(o => o.OrderItems);
        }
        // Retrieve All Orders (Include[OrderItems, DeliveryMethods]) by UserEmail ==> Return List of Objects(Collection)
        public OrderWithIncludesSpecifications(string userEmail) : base(x => x.UserEmail == userEmail)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethods);
            SetOrderBy(o => o.OrderDate);
        }
    }
    {

    }
}

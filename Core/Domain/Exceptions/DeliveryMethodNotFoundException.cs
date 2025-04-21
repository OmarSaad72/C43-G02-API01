using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException : NotFoundException
    {
        public DeliveryMethodNotFoundException(int deliveryMethodId) : base($"Delivery method with id {deliveryMethodId} not found.")
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public record ShippingAddressDto
    {
        public string FName { get; init; }
        public string LName { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Street { get; init; }
    }
}

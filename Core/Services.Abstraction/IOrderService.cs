using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        public Task<OrderResultDto> GetOrderByIdAsync(Guid id);
        public Task<IEnumerable<OrderResultDto>> GetAllOrdersByEmailAsync(string userEmail);
        public Task<OrderResultDto> CreateOrderAsync(OrderRequest request, string userEmail);
        public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}

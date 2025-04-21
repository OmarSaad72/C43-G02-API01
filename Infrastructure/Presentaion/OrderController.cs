using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs;
using System.Security.Claims;

namespace Presentation
{
    //baseUrl/api/orderController
    public class OrderController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> Create(OrderRequest orderRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManager.OrderService.CreateOrderAsync(orderRequest, email);
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetAllOrdersByEmail()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManager.OrderService.GetAllOrdersByEmailAsync(email);
            return Ok(order);
        }
        [HttpGet("{id}")] // Dynamic Segment ==> Entire The Value
        public async Task<ActionResult<OrderResultDto>> GetOrderById(Guid id)
        {
            var order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        [HttpGet("DeliveryMethod")] // Static Segment
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        {
            return Ok(await serviceManager.OrderService.GetDeliveryMethodsAsync());
        }

    }
}

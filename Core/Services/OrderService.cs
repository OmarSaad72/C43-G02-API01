using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Services.Abstraction;
using Services.Specifications;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class OrderService(IMapper mapper, IBasketRepo basketRepo, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            var shippingAddress = mapper.Map<Domain.Entities.OrderEntities.Address>(request.ShippingAddress);
            var basket = await basketRepo.GetBasketAsync(request.BaskketId) ??
                throw new BasketNotFoundException(request.BaskketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id) ??
                    throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethods, int>()
                .GetByIdAsync(request.DeliveryMethodId) ??
                throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);
            var subtotal = orderItems.Sum(x => x.Price * x.Quantity);
            var order = new Order(userEmail, shippingAddress, orderItems, deliveryMethod, subtotal); // Create Order
            // Add Order to Repository ==> save in DB
            await unitOfWork.GetRepository<Order, Guid>().Add(order);
            await unitOfWork.SaveChangesAsync();
            // Map & return 
            return mapper.Map<OrderResultDto>(order);
        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
           => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), item.Quantity, product.Price);


        public async Task<IEnumerable<OrderResultDto>> GetAllOrdersByEmailAsync(string userEmail)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludesSpecifications(userEmail)) ??
                throw new OrderNotFoundException(userEmail);
            return mapper.Map<IEnumerable<OrderResultDto>>(orders);
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithIncludesSpecifications(id)) ??
                throw new OrderNotFoundException(id);
            return mapper.Map<OrderResultDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethods, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }

    }
}

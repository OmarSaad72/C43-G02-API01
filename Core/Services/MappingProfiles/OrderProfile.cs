using AutoMapper;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Domain.Entities.OrderEntities.Address, ShippingAddressDto>();
            CreateMap<DeliveryMethods, DeliveryMethodResult>();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(src => src.Product.ProductName))
                .ForMember(d => d.ProductId, o => o.MapFrom(src => src.Product.ProductId))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(src => src.Product.PictureUrl));
            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.PaymentStatus, o => o.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.DeliveryMethods, o => o.MapFrom(src => src.DeliveryMethods.ShortName))
                .ForMember(dest => dest.Total, o => o.MapFrom(src => src.Subtotal + src.DeliveryMethods.Price));
        }
    }
}

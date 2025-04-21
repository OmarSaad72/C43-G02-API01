using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService>  _basketService;
        private readonly Lazy<IAuthenticationService>  _authenticationService;
        private readonly Lazy<IOrderService>  _orderService;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepo basketRepo, UserManager<User> userManager, IOptions<JwtOptions> options)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepo, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,mapper ,options));
            _orderService = new Lazy<IOrderService>(() => new OrderService(mapper, basketRepo, unitOfWork));
        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IOrderService OrderService => _orderService.Value;
    }
}

using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;

namespace Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        }
        public IProductService ProductService => _productService.Value;
    }
}

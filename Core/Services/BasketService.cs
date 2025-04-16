using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstraction;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepo _basketRepo, IMapper _mapper) : IBasketService
    {
        public async Task<bool> deleteBasketAsync(string id)
            => await _basketRepo.DeleteBasketAsync(id);

        public async Task<BasketDto?> getBasketAsync(string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) : _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto?> updateBasketAsync(BasketDto basketDto)
        {
            var customerBasket = await _basketRepo.UpdateBasketAsync(_mapper.Map<CustomerBasket>(basketDto));
            return customerBasket is null ? throw new Exception("Can't Update, Please Try Again Later") : _mapper.Map<BasketDto>(customerBasket);
        }
    }
}

using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IBasketService
    {
        Task<BasketDto?> getBasketAsync(string id);
        Task<bool> deleteBasketAsync(string id);
        Task<BasketDto?> updateBasketAsync(BasketDto basketDto);
    }
}

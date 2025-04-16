using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepo
    {
        public Task<CustomerBasket?> GetBasketAsync(int id);
        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket, TimeSpan? timeToLive = null);
        public Task<bool> DeleteBasketAsync(string id);
    }
}

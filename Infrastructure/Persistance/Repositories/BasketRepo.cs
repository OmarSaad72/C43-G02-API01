using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class BasketRepo(IConnectionMultiplexer connection) : IBasketRepo
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string id)
        {
          return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var value = await _database.StringGetAsync(id);
            if(value.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket>(value);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket, TimeSpan? timeToLive = null)
        {
            var json = JsonSerializer.Serialize(customerBasket);
            var iscreatedorupdated = await _database
                .StringSetBitAsync(customerBasket.Id, json, timeToLive ?? TimeSpan.FromDays(30));
            return iscreatedorupdated ? await GetBasketAsync(customerBasket.Id) : null;
        }
    }
}

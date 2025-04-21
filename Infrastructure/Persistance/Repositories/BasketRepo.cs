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
        private readonly IDatabase _database = connection.GetDatabase(); // through this field, i can deal with DB
        public async Task<bool> DeleteBasketAsync(string id)
        => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var value = await _database.StringGetAsync(id);  // data ==> json
            if (value.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket?>(value); // retrieve ==> C# {Deserialize}
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            // add or update ==> json 
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLive ?? TimeSpan.FromDays(15));
            return isCreatedOrUpdated ? await GetBasketAsync(basket.Id) : null;
        }
    }
}

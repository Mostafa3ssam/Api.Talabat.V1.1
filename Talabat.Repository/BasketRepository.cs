using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Interfaces;

namespace Talabat.Repository
{
   public  class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetCustomerBasketAsync(string BasketId)
        {
            var Basket = await _database.StringGetAsync(BasketId);
            if (Basket.IsNull) return null;
            else
            {
                return JsonSerializer.Deserialize<CustomerBasket>(Basket);
            }
            
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var CreateOrUpdate = await _database.StringSetAsync(basket.Id, JsonBasket ,TimeSpan.FromDays(1));
            if (!CreateOrUpdate) return null;
            else
            {
                return await GetCustomerBasketAsync(basket.Id);
            }
        }
    }
}

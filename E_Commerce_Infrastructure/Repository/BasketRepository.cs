using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.Entities.Baskets;
using StackExchange.Redis;

namespace E_Commerce_Infrastructure.Repository
{
    //Redis Package
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();   
        }
        public async Task<CustomerBasket?> CreateOrUpdateBaketAsync(CustomerBasket basket, TimeSpan? Timetolive = null, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(basket);
            var success = await _database.StringSetAsync(basket.Id, json, Timetolive ?? TimeSpan.FromDays(30));
            return success ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string Id, CancellationToken ct = default)
        {
            return await _database.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
            var basket = await _database.StringGetAsync(basketId);
            if (basket.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<CustomerBasket>(basket.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Contracts;
using StackExchange.Redis;

namespace E_Commerce_Infrastructure.Repository
{
    public class CacheRepository : ICacheRepository

    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database=connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string cashekey, CancellationToken ct = default)
        {
            var value = await _database.StringGetAsync(cashekey);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string cashekey, string cashvalue, TimeSpan timeTolive, CancellationToken ct = default)
        {
            await _database.StringSetAsync(cashekey, cashvalue, timeTolive);

        }
    }
}

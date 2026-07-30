using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce_Application.Contracts;

namespace E_Commerce_Application.Service
{
    public class CacheService :ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public Task<string?> GetAsync(string cashkey, CancellationToken ct = default) {

            return _cacheRepository.GetAsync(cashkey, ct);

        }
        public Task SetAsync(string cashkey, object cachevalue, TimeSpan timeToLive, CancellationToken ct = default) {

            var json = JsonSerializer.Serialize(cachevalue, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return _cacheRepository.SetAsync(cashkey, json, timeToLive, ct);
        }










    }
}

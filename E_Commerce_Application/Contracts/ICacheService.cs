using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Contracts
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string cashkey, CancellationToken ct = default);
        Task SetAsync(string cashkey, object cachevalue, TimeSpan timeToLive, CancellationToken ct = default);

    }
}

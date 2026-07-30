using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string cashekey, CancellationToken ct = default);
        Task SetAsync(string cashekey, string cashvalue, TimeSpan timeTolive, CancellationToken ct = default);
    }
}

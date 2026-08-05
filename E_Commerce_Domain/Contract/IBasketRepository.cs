using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities.Baskets;

namespace E_Commerce_Domain.Contract
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default);
        Task<CustomerBasket?> CreateOrUpdateBaketAsync(CustomerBasket basket, TimeSpan? Timetolive = null, CancellationToken ct = default);
        Task<bool> DeleteBasketAsync(string Id, CancellationToken ct = default);
    }
}

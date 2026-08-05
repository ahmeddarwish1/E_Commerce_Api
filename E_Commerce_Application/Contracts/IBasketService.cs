using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Common;
using E_Commerce_Application.Dtos.Baskets;

namespace E_Commerce_Application.Contracts
{
    public interface IBasketService
    {
        Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken cancellationToken = default);
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken cancellationToken = default);




    }
}

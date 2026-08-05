using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Baskets;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.Entities.Baskets;

namespace E_Commerce_Application.Service
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {

        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken cancellationToken = default)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);
            var basketResult = await basketRepository.CreateOrUpdateBaketAsync(customerBasket);
            return basketResult != null ? Result<BasketDto>.Ok(mapper.Map<BasketDto>(basketResult)) : Result<BasketDto>.
            Fail(Error.Failure("BasketDelete.Failure", "Can Not Delete Basket"));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken cancellationToken = default)
        {

            var basket = await basketRepository.GetBasketAsync(id, cancellationToken);
            if (basket == null)
                return Result<BasketDto>.Fail(Error.NotFound("Basket Not Found"));
            return Result<BasketDto>.Ok(mapper.Map<BasketDto>(basket));

        }
        public async Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken cancellationToken = default)
        {

            var result = await basketRepository.DeleteBasketAsync(id, cancellationToken);
            return result ? Result<bool>.Ok(true) : Result<bool>.
            Fail(Error.Failure("BasketDelete.Failure", "Can Not Delete Basket"));
        }




    }
}
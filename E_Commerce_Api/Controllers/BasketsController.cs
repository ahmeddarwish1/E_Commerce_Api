using System.Threading;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Api.Controllers
{
    public class BasketsController : ApiBaseController
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BasketDto>> GetBasket(string id, CancellationToken cancellationToken)
        {
            var basket = await _basketService.GetBasketAsync(id, cancellationToken);
            return ToActionResult(basket);
        }

        [HttpPost]

        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket, CancellationToken cancellationToken)
        {

            var saved = await _basketService.CreateOrUpdateBasketAsync(basket, cancellationToken);
            return ToActionResult(saved);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken cancellationToken)

        {
            var result = await _basketService.DeleteBasketAsync(id, cancellationToken);
            return ToActionResult(result);

        }
    }
}

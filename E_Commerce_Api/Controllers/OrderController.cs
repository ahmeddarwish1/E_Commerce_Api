using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Api.Controllers
{
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder([FromBody] OrderDto orderDto, [FromQuery] string email, CancellationToken ct)
        {
            return ToActionResult(await orderService.CreatOrderAsync(orderDto, email, ct));
        }
    }
}

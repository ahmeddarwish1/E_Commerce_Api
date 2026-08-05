using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Common;
using E_Commerce_Application.Dtos.Orders;

namespace E_Commerce_Application.Contracts
{
    public interface IOrderService
    {
        Task<Result<OrderToReturnDto>> CreatOrderAsync(OrderDto orderDto, string email, CancellationToken cancellationToken);

    }
}

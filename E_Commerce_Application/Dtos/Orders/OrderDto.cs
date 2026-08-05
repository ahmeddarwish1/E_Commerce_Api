using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Dtos.Orders
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; } = default!;
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; } = default!;
    }
}

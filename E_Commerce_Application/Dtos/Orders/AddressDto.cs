using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Dtos.Orders
{
    public class AddressDto
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Street { get; set; } = default!;

        public string City { get; set; } = default!;

        public string Country { get; set; } = default!;
    }
}

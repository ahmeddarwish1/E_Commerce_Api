using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities.Baskets;

namespace E_Commerce_Application.Dtos.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItemDto> Items { get; set; } = [];
    }
}
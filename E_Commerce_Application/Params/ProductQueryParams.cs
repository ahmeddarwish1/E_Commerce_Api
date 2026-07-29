using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Params
{
    public class ProductQueryParams
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string? SearchValue { get; set; }

        public ProductSortingOption Sort { get; set; }

        public int PageIndex { get; set; } = 1;
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        private int pageSize = DefaultPageSize;//5
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : (value < 1 ? DefaultPageSize : value);
        }
    }
}

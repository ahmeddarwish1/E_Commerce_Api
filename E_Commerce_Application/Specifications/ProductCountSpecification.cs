using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Params;
using E_Commerce_Domain.Entities.Products;

namespace E_Commerce_Application.Specifications
{
    public class ProductCountSpecification:BaseSpecifications<Product,int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) : base
        (p =>
        (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId)
        && (!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId)
        && (string.IsNullOrEmpty(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))
        )
        {

        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Params;
using E_Commerce_Domain.Entities.Products;

namespace E_Commerce_Application.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications(ProductQueryParams queryParams) :
        base
        (p =>
        (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId)
        && (!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId)
        &&(string.IsNullOrEmpty(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))
        )
        //true & True == >13
        //value& true == > brand
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (queryParams.Sort)
            {

                case ProductSortingOption.NameAsc  :AddOrderBy(P => P.Name); break;
                case ProductSortingOption.NameDesc :AddOrderByDesc(P => P.Name); break;
                case ProductSortingOption.PriceAsc :AddOrderBy(P => P.Price); break;
                case ProductSortingOption.PriceDesc:AddOrderByDesc(P => P.Price); break;
                                                  _:AddOrderBy(P => P.Name); break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
        public ProductSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

    }
}

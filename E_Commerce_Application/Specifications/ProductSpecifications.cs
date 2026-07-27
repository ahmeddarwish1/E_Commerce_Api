using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities.Products;

namespace E_Commerce_Application.Specifications
{
    public class ProductSpecifications :BaseSpecifications<Product,int> 
    {
        public ProductSpecifications(int? brandId, int? typeId) :base(p => (!brandId.HasValue || p.BrandId == brandId) 
        && (!typeId.HasValue || p.TypeId == typeId))
        //true & True == >13
        //value& true == > brand
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
        public ProductSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

    }
}

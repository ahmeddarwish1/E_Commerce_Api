using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities.Products;

namespace E_Commerce_Application.Specifications
{
    public class ProductWithIdsSpecifications :BaseSpecifications<Product,int>
    {
        public ProductWithIdsSpecifications(IEnumerable<int> Ids) : base(P => Ids.Contains(P.Id)) 
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities;

namespace E_Commerce_Domain.Contract
{
    public  interface ISpecifications<TEntity,Tkey>where TEntity : BaseEntity<Tkey>
    {
        List<Expression<Func<TEntity,object>>> IncludeExpression { get; }

        Expression<Func<TEntity, bool>> Criteria { get; }
    }
}

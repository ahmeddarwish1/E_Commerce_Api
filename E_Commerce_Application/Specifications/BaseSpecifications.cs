using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.Entities;

namespace E_Commerce_Application.Specifications
{
    public abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; private set; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria = null)
        {
            Criteria = criteria;
        }

        public void AddInclude(Expression<Func<TEntity, object>> expression)
        {

            IncludeExpression.Add(expression);
        }


    }
}

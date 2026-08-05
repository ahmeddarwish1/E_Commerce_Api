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

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; private set; } = [];
        public void AddInclude(Expression<Func<TEntity, object>> expression)
        {

            IncludeExpression.Add(expression);
        }

        #endregion
        #region Criteria
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria = null)
        {
            Criteria = criteria;
        }
        #endregion

          
        #region OrderBy
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public void AddOrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }
        public void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }

        #endregion
        #region Pagination
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }
        //10
        public void ApplyPagination(int pageSize,int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
        #endregion




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Infrastructure.Repository
{
    //dynamic query
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> Inputquery, ISpecifications<TEntity, Tkey> specifications)
            where TEntity : BaseEntity<Tkey>
        {
            #region Include
            var query = Inputquery;
            if (specifications.IncludeExpression.Count > 0)
            {
                //dbcontext.set<T>().include(brand).include(types)
                query = specifications.IncludeExpression.Aggregate(query, (current, experrsion) => current.Include(experrsion));
            }
            #endregion
            #region Criteria
            if (specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);
            #endregion
            #region OrderBy

            if (specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);
            if (specifications.OrderByDesc is not null)
                query = query.OrderByDescending(specifications.OrderByDesc);

            #endregion
            if (specifications.IsPaginated)
                query = query.Skip(specifications.Skip).Take(specifications.Take);


            return query;


        } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.Entities;
using E_Commerce_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Infrastructure.Repository
{
    internal class GenricRepository<TEntity, Tkey>(StoreDbContext dbContext) : IGenricRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public void Add(TEntity entity)
        => dbContext.Set<TEntity>().Add(entity);
        public void Delete(TEntity entity)
        => dbContext.Set<TEntity>().Remove(entity);
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
        => await dbContext.Set<TEntity>().ToListAsync(ct);
        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
        => await dbContext.Set<TEntity>().FindAsync(id, ct);
        public void Update(TEntity entity)
        => dbContext.Set<TEntity>().Update(entity);



        public async Task<IReadOnlyList<TEntity>> GetAllwithspecAsync(ISpecifications<TEntity, Tkey> spec, CancellationToken ct = default)
        {
            //dbcontext.set<T> Iqueryable||spec
            var result=SpecificationEvaluator.CreateQuery<TEntity,Tkey>(dbContext.Set<TEntity>(), spec);
            return await result.ToListAsync();
        }

        public async Task<TEntity?> GetByIdwithspecAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default)
        {
            var result = SpecificationEvaluator.CreateQuery<TEntity, Tkey>(dbContext.Set<TEntity>(), specifications);
            return await result.FirstOrDefaultAsync(ct);
        }

        public async Task<int> GetProductCountwithspecAsync(ISpecifications<TEntity, Tkey> spec, CancellationToken ct = default)
        {
            var result = SpecificationEvaluator.CreateQuery<TEntity, Tkey>(dbContext.Set<TEntity>(), spec);
            return await result.CountAsync(ct);
        }
    }
}

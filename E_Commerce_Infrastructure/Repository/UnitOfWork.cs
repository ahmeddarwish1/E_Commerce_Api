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
    public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {

        private readonly Dictionary<string, object> repositories = [];
        public IGenricRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var typename = typeof(TEntity).Name;
            if (repositories.TryGetValue(typename, out object? value))
                return (IGenricRepository<TEntity, Tkey>)value;
            else
            {
                var repo = new GenricRepository<TEntity, Tkey>(dbContext);
                repositories[typename] = repo;
                return repo;
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
=> dbContext.SaveChangesAsync(ct);
    }
}

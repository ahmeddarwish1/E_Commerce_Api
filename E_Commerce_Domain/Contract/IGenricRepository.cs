using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities;

namespace E_Commerce_Domain.Contract
{
    public interface IGenricRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllwithspecAsync(ISpecifications<TEntity,Tkey> spec,CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<TEntity?> GetByIdwithspecAsync(ISpecifications<TEntity,Tkey>specifications, CancellationToken ct = default);
    }
}
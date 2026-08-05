using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities;

namespace E_Commerce_Domain.Contract
{
    public interface IUnitOfWork
    {
        Task<int>SaveChangesAsync(CancellationToken ct = default);
        IGenricRepository<TEntity,Tkey>GetRepository<TEntity,Tkey>()where TEntity : BaseEntity<Tkey>;

    }
}

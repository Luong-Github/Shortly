using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Contract.DependencyInjections.Repositories
{
    public interface IRepositoryDbContext<TDbContext, TEntity, in TKey>
        where TDbContext : DbContext
        where TEntity : class
    {
        TEntity GetByID(TKey id);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includesParams);

        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includesParams);

        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includesParams);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);

        void RemoveMultiple(List<TEntity> entities);
    }
}

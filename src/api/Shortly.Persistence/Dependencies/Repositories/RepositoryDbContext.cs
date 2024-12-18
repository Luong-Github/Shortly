﻿using Microsoft.EntityFrameworkCore;
using Shortly.Contract.DependencyInjections.Repositories;
using Shortly.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Persistence.Dependencies.Repositories
{
    public class RepositoryDbContext<TDbContext, TEntity, TKey> : IRepositoryDbContext<TDbContext, TEntity, TKey>, IDisposable
        where TDbContext : DbContext
        where TEntity : EntityBase<TKey> where TKey : class
    {
        private readonly TDbContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public RepositoryDbContext(TDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includesParams)
        {
            IQueryable<TEntity> entities = _dbSet.AsNoTracking();
            IQueryable<TEntity> items = _dbSet.AsNoTracking();
            if (includesParams != null)
            {
                foreach (var includesParam in includesParams)
                {
                    items = items.Include(includesParam);
                }
            }

            if (predicate != null)
                items = items.Where(predicate);

            return items;
        }

        public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includesParams)
        {
            return await FindAll(null, includesParams)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includesParams)
            => await FindAll(predicate, includesParams).FirstOrDefaultAsync(cancellationToken);

        public TEntity GetByID(TKey id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            return _dbSet.Find(id);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void RemoveMultiple(List<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _dbSet.RemoveRange(entities);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "The entity to update cannot be null.");
            }

            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}

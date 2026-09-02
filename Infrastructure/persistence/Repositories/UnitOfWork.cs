using Domain.Contracts;
using Domain.Entities;
using persistence.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private ConcurrentDictionary<string, object> _Repositories;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _Repositories = new();
        }

        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        => (IGenaricRepository<TEntity, TKey>)_Repositories.GetOrAdd(typeof(TEntity).Name
            ,(_) => new GenaricRepository<TEntity, TKey>(_dbContext));

        public async Task<int> SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
    }
}

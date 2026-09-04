using Domain.Contracts;
using Domain.Entities;
using persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace persistence.Repositories
{
    public class GenaricRepository<TEntity, TKey>(StoreDbContext _dbContext)
        : IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>

    {
        //Add 
        public async Task AddAsync(TEntity entity)
        => await _dbContext.Set<TEntity>().AddAsync(entity);

        // Delete
        public void Delete(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);

        //Get All
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTraking = false)
        => AsNoTraking ? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync() 
            : await _dbContext.Set<TEntity>().ToListAsync();

       

        // Get By ID 
        public async Task<TEntity?> GetByIdAsync(TKey id)
        => await _dbContext.Set<TEntity>().FindAsync(id);

        // Update
        public void Update(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);


        #region Specifications
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>() ,specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
       => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();

        #endregion
    }
}

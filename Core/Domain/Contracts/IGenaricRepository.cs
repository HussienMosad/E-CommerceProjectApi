using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenaricRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        //GetAll
        Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTraking = false);

        // Get By Id
        Task<TEntity?> GetByIdAsync(TKey id);

        //Create
        Task AddAsync(TEntity entity);
        //Update
        void Update(TEntity entity);
        //Delete

        void Delete(TEntity entity);
    }
}

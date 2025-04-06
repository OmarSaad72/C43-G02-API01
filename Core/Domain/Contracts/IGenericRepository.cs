using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity?> GetByIdAsync(TKey Id);
        Task <IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking= false);
        Task<TEntity?> GetByIdAsync(Specifications<TEntity> specifications);
        Task <IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications);
        Task Add(TEntity entity);   
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}

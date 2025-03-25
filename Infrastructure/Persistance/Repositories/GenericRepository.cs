namespace Persistance.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking)
            => AsNoTracking ? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync() // True
            : await _dbContext.Set<TEntity>().ToListAsync();  // False

        public async Task<TEntity?> GetByIdAsync(TKey Id)
            => await _dbContext.Set<TEntity>().FindAsync(Id);
    }
}

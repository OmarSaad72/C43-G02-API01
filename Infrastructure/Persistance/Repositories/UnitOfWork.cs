namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private ConcurrentDictionary<string, object> _repositories;
        //private Dictionary<string, object> _repositories;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>

            //return new GenericRepository<TEntity, TKey>(_dbContext); // Old Implementation(Invalid) ==> Because User Can Create More Instance In The Same Request

            //var typeName = typeof(TEntity).Name;
            //if (_repositories.ContainsKey(typeName))
            //    return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            //else
            //{
            //    var repo = new GenericRepository<TEntity, TKey>(_dbContext);
            //    _repositories.Add(typeName, repo); // Key , Value
            //    return repo;   // With Dictionary
            //}

            => (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity)
                .Name, (_) => new GenericRepository<TEntity, TKey>(_dbContext));  // With ConcurrentDictionary


        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}

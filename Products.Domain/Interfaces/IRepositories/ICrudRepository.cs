using Products.Domain.Entities.Db;

namespace Products.Domain.Interfaces.IRepositories;

public interface ICrudRepository<TEntity,TKey> where TEntity:Base<TKey>
{
    Task Create(TEntity entity,CancellationToken cancellationToken);
    Task<TEntity> GetById(TKey key,CancellationToken cancellationToken);
    Task Update(TEntity entity,TKey key,CancellationToken cancellationToken);
    Task Delete(TKey key,CancellationToken cancellationToken);
    
    Task<IEnumerable<TEntity>> GetList(CancellationToken cancellationToken);
}
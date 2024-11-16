using Products.Domain.Entities.Db;

namespace Products.Domain.Interfaces.IRepositories;

public interface ICrudRepository<TEntity,TKey> where TEntity:Base<TKey>
{
    Task Create(TEntity entity,CancellationToken cancellationToken=default);
    Task<TEntity> GetById(TKey key,CancellationToken cancellationToken=default);
    Task Update(TEntity entity,TKey key,CancellationToken cancellationToken=default);
    Task Delete(TKey key,CancellationToken cancellationToken=default);
    
    Task<IEnumerable<TEntity>> GetList(CancellationToken cancellationToken=default);
}
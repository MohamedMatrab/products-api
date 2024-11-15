using Products.Domain.Entities.Db;

namespace Products.Domain.Interfaces.IRepositories;

public interface IRepository<TEntity,TKey> where TEntity:Base<TKey>
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
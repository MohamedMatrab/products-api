using Products.Domain.Common;
using Products.Domain.Entities.Db;

namespace Products.Domain.Interfaces.IRepositories;

public interface IHasPaginatedList<TEntity,TKey> where TEntity:Base<TKey>
{
    Task<IEnumerable<TEntity>> PaginatedList(PaginationParams paginationParams,CancellationToken cancellationToken=default);
}
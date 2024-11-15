using Products.Domain.Entities.Common;
using Products.Domain.Entities.Db;

namespace Products.Domain.Interfaces.IRepositories;

public interface IHasPaginatedList<TEntity,TKey> where TEntity:Base<TKey>
{
    Task<IEnumerable<TEntity>> PaginatedList(int pageNumber, int pageSize,CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> PaginatedList(int pageNumber, int pageSize,string sortBy,CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> PaginatedList(int pageNumber, int pageSize,string sortBy,List<ExpressionFilter> filters,CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> PaginatedList(int pageNumber, int pageSize,string sortBy, string sortOrder,List<ExpressionFilter> filters,CancellationToken cancellationToken);
}
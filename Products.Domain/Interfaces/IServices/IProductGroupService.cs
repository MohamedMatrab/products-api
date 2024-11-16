using Products.Domain.Common;
using Products.Domain.DTOS;
using Products.Domain.DTOS.ProductGroups.Request;
using Products.Domain.DTOS.ProductGroups.Response;

namespace Products.Domain.Interfaces.IServices;

public interface IProductGroupService<TKey>
{
    Task<Result> Add(GroupDto dto,CancellationToken cancellationToken=default);
    Task<Result> Update(GroupDto dto, TKey key, CancellationToken cancellationToken = default);
    Task<Result> DeleteRange(IEnumerable<TKey> keys,CancellationToken cancellationToken=default);
    Task<IEnumerable<GroupResponse>> GetList(PaginationParams? paginationParams=null,CancellationToken cancellationToken=default);
    Task<GroupDto> GetById(TKey key,CancellationToken cancellationToken=default);
}
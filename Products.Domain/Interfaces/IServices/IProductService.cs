using Products.Domain.Common;
using Products.Domain.DTOS;
using Products.Domain.DTOS.Product.Request;
using Products.Domain.DTOS.Product.Response;

namespace Products.Domain.Interfaces.IServices;

public interface IProductService<TKey>
{
    Task<Result> AddProduct(ProductDto dto,CancellationToken cancellationToken=default);
    Task<Result> UpdateProduct(ProductDto dto, TKey key, CancellationToken cancellationToken = default);
    Task<Result> DeleteRange(IEnumerable<TKey> keys,CancellationToken cancellationToken=default);
    Task<IEnumerable<ProductResponse>> GetList(PaginationParams? paginationParams=null,CancellationToken cancellationToken=default);
    Task<ProductDto> GetById(TKey key,CancellationToken cancellationToken=default);
}
using Products.Domain.DTOS.Product.Response;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IMappers;

namespace Products.Domain.Mappers.Products;

public class ProductModelToResponse : IListMapper<Product,ProductResponse>
{
    public IEnumerable<ProductResponse> MapList(IEnumerable<Product> source)
    {
        return source.Select(e => new ProductResponse
        {
            Id = e.Id,
            Price = e.Price,
            Description = e.Description,
            Quantity = e.Quantity,
            Code = e.Code,
            Name = e.Name,
            IsActive = e.IsActive
        });
    }
}
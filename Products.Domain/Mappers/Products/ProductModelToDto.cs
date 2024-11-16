using AutoMapper;
using Products.Domain.DTOS.Product.Request;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IMappers;

namespace Products.Domain.Mappers.Products;

public class ProductModelToDto(IMapper mapper) : IEntityMapper<Product, ProductDto>
{
    public ProductDto MapModel(Product source)
    {
        return mapper.Map<ProductDto>(source);
    }

    public ProductDto MapModel(Product source, ProductDto destination)
    {
        return mapper.Map(source, destination);
    }
}
using AutoMapper;
using Products.Domain.DTOS.Product.Request;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IMappers;

namespace Products.Domain.Mappers.Products;

public class ProductDtoToModel(IMapper mapper) : IEntityMapper<ProductDto,Product>
{
    public Product MapModel(ProductDto source)
    {
        return mapper.Map<Product>(source);
    }
    public Product MapModel(ProductDto source, Product destination)
    {
        return mapper.Map(source,destination);
    }
}
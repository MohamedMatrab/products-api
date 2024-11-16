using Products.Domain.DTOS.Product.Request;
using Products.Domain.DTOS.Product.Response;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IMappers;
using Products.Domain.Interfaces.IServices;

namespace Products.Data.Services;

public class ProductService(IEntityMapper<Product,ProductDto> mapper,IListMapper<Product,ProductResponse> responseMapper) 
    : IProductService
{
    
}
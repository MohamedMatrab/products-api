using AutoMapper;
using Products.Domain.DTOS.Product.Request;
using Products.Domain.DTOS.Product.Response;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IMappers;
using Products.Domain.Mappers.Products;

namespace products_api.Extensions;

public static class MapperExtension
{
    public static IServiceCollection RegisterMapperService(this IServiceCollection services)
    {

        #region Mapper
        services.AddSingleton<IMapper>(_ => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductDto>();
            cfg.CreateMap<ProductDto, Product>();
            cfg.CreateMap<Product, ProductResponse>();
        }).CreateMapper());
        
        // Register the IMapperService implementation with your dependency injection container
        services.AddSingleton<IEntityMapper<Product,ProductDto>,ProductModelToDto>();
        services.AddSingleton<IListMapper<Product,ProductResponse>,ProductModelToResponse>();
        services.AddSingleton<IEntityMapper<ProductDto,Product>,ProductDtoToModel>();
        
        #endregion

        return services;
    }
}
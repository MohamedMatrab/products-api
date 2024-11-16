using Products.Data.Services;
using Products.Domain.Interfaces.IRepositories;
using Products.Domain.Interfaces.IServices;
using Products.Infrastructure.Repositories;

namespace products_api.Extensions;

public static class ServiceExtension
{
    public static void RegisterService(this IServiceCollection services)
    {
        #region Services
        services.AddScoped<IProductService<int>, ProductService>();

        #endregion

        #region Repositories
        services.AddTransient<IProductRepository, ProductRepository>();

        #endregion
    }
}
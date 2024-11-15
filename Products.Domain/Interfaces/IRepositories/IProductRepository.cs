using Products.Domain.Entities.Db;

namespace Products.Domain.Interfaces.IRepositories;

public interface IProductRepository : IRepository<Product,int>,
    ICrudRepository<Product,int>,IHasPaginatedList<Product,int>
{
    
}
using Products.Domain.Entities.Db;

namespace Products.Domain.Interfaces.IRepositories;

public interface IGroupsRepository:IRepository<ProductsGroup,int>,
    ICrudRepository<ProductsGroup,int>,IHasPaginatedList<ProductsGroup,int>
{
    
}
using Products.Domain.DTOS.ProductGroups.Response;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IMappers;

namespace Products.Domain.Mappers.ProductsGroups;

public class GroupModelToRsp : IListMapper<ProductsGroup,GroupResponse>
{
    public IEnumerable<GroupResponse> MapList(IEnumerable<ProductsGroup> source)
    {
        return source.Select(e => new GroupResponse
        {
            Id = e.Id,
            Label = e.Label
        });
    }
}
using AutoMapper;
using Products.Domain.DTOS.ProductGroups.Request;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IMappers;

namespace Products.Domain.Mappers.ProductsGroups;

public class GroupModelToDto(IMapper mapper) : IEntityMapper<ProductsGroup, GroupDto>
{
    public GroupDto MapModel(ProductsGroup source)
    {
        return mapper.Map<GroupDto>(source);
    }

    public GroupDto MapModel(ProductsGroup source, GroupDto destination)
    {
        return mapper.Map(source,destination);
    }
}
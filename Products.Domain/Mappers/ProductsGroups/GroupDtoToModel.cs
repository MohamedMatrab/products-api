using AutoMapper;
using Products.Domain.DTOS.ProductGroups.Request;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IMappers;

namespace Products.Domain.Mappers.ProductsGroups;

public class GroupDtoToModel(IMapper mapper) : IEntityMapper<GroupDto,ProductsGroup>
{
    public ProductsGroup MapModel(GroupDto source)
    {
        return mapper.Map<ProductsGroup>(source);
    }

    public ProductsGroup MapModel(GroupDto source, ProductsGroup destination)
    {
        return mapper.Map(source,destination);
    }
}
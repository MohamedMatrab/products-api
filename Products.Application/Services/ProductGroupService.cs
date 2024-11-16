using Products.Domain.Common;
using Products.Domain.DTOS;
using Products.Domain.DTOS.ProductGroups.Request;
using Products.Domain.DTOS.ProductGroups.Response;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IMappers;
using Products.Domain.Interfaces.IRepositories;
using Products.Domain.Interfaces.IServices;

namespace Products.Data.Services;

public class ProductGroupService(IEntityMapper<ProductsGroup,GroupDto> mapperToDto,
    IEntityMapper<GroupDto,ProductsGroup> mapper,
    IListMapper<ProductsGroup,GroupResponse> responseMapper, 
    IGroupsRepository groupsRepository) : IProductGroupService<int>
{
    public async Task<Result> Add(GroupDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var product = mapper.MapModel(dto);
            await groupsRepository.Create(product,cancellationToken);
            return new Result {Success = true};
        }
        catch(Exception e)
        {
            return new Result { Success = false, Errors = [e.Message] };
        }
    }

    public async Task<Result> Update(GroupDto dto, int key, CancellationToken cancellationToken = default)
    {
        try
        {
            var product = mapper.MapModel(dto);
            await groupsRepository.Update(product,key,cancellationToken);
            return new Result {Success = true};
        }
        catch(Exception e)
        {
            return new Result { Success = false, Errors = [e.Message] };
        }
    }

    public async Task<Result> DeleteRange(IEnumerable<int> keys, CancellationToken cancellationToken = default)
    {
        var res = new Result();
        foreach (var key in keys)
        {
            try
            {
                await groupsRepository.Delete(key,cancellationToken);
            }
            catch(Exception e)
            {
                res.Errors.Add(e.Message);
            }
        }

        res.Success = res.Errors.Count == 0;
        return res;
    }

    public async Task<IEnumerable<GroupResponse>> GetList(PaginationParams? paginationParams = null, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<ProductsGroup> list;
            if (paginationParams is null)
                list = await groupsRepository.GetList(cancellationToken);
            else
                list = await groupsRepository.PaginatedList(paginationParams,cancellationToken);
            return responseMapper.MapList(list);
        }
        catch (Exception e)
        {
            throw new Exception("Exception While Getting List Of Groups !");
        }
    }

    public async Task<GroupDto> GetById(int key, CancellationToken cancellationToken = default)
    {
        try
        {
            var obj = await groupsRepository.GetById(key, cancellationToken);
            return mapperToDto.MapModel(obj);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
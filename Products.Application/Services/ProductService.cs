using Products.Domain.Common;
using Products.Domain.DTOS;
using Products.Domain.DTOS.Product.Request;
using Products.Domain.DTOS.Product.Response;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IMappers;
using Products.Domain.Interfaces.IRepositories;
using Products.Domain.Interfaces.IServices;

namespace Products.Data.Services;

public class ProductService(IEntityMapper<Product,ProductDto> mapperToDto,
    IEntityMapper<ProductDto,Product> mapper,
    IListMapper<Product,ProductResponse> responseMapper, 
    IProductRepository productRepository) 
    : IProductService<int>
{
    public async Task<Result> AddProduct(ProductDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var product = mapper.MapModel(dto);
            await productRepository.Create(product,cancellationToken);
            return new Result {Success = true};
        }
        catch(Exception e)
        {
            return new Result { Success = false, Errors = [e.Message] };
        }
    }

    public async Task<Result> UpdateProduct(ProductDto dto, int key, CancellationToken cancellationToken = default)
    {
        try
        {
            var product = mapper.MapModel(dto);
            await productRepository.Update(product,key,cancellationToken);
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
                await productRepository.Delete(key,cancellationToken);
            }
            catch(Exception e)
            {
                res.Errors.Add(e.Message);
            }
        }

        res.Success = res.Errors.Count == 0;
        return res;
    }
    
    public async Task<IEnumerable<ProductResponse>> GetList(PaginationParams? paginationParams=null, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Product> list;
            if (paginationParams is null)
                list = await productRepository.GetList(cancellationToken);
            else
                list = await productRepository.PaginatedList(paginationParams,cancellationToken);
            return responseMapper.MapList(list);
        }
        catch (Exception e)
        {
            throw new Exception("Exception While Getting List Of Products !");
        }
    }

    public async Task<ProductDto> GetById(int key,CancellationToken cancellationToken=default)
    {
        try
        {
            var obj = await productRepository.GetById(key, cancellationToken);
            return mapperToDto.MapModel(obj);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
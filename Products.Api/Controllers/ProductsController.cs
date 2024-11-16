using Microsoft.AspNetCore.Mvc;
using Products.Domain.Common;
using Products.Domain.DTOS;
using Products.Domain.DTOS.Product.Request;
using Products.Domain.DTOS.Product.Response;
using Products.Domain.Interfaces.IServices;

namespace products_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService<int> productService) : ControllerBase
{
    [HttpGet("get-products")]
    public async Task<IActionResult> GetProducts(int? pageNumber,int? pageSize,string? sortBy,string? sortOrder,
        CancellationToken cancellationToken)
    {
        try
        {
            Thread.Sleep(20000);
            IEnumerable<ProductResponse> list;
            if (!pageNumber.HasValue)
                list= await productService.GetList(null,cancellationToken);
            else
                list = await productService.GetList(new PaginationParams
                {
                    PageNumber = pageNumber.Value,
                    PageSize = pageSize ?? 10,
                    SortBy = sortBy,
                    SortOrder = sortOrder,
                    Filters = []
                }, cancellationToken);
            var materializedList = list.ToList();
            return Ok(new ListResult<ProductResponse>
            {
                PageNumber = pageNumber ?? 1,
                ResultsCount = materializedList.Count,
                Results = materializedList
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct([FromBody]ProductDto dto,CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await productService.AddProduct(dto,cancellationToken));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("edit-product/{id:int}")]
    public async Task<IActionResult> EditProduct([FromBody]ProductDto dto,int id,CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await productService.UpdateProduct(dto,id,cancellationToken));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("delete-products")]
    public async Task<IActionResult> DeleteRange([FromBody]List<int> ids,CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await productService.DeleteRange(ids,cancellationToken));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
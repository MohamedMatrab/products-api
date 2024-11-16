using Microsoft.AspNetCore.Mvc;
using Products.Domain.Common;
using Products.Domain.DTOS;
using Products.Domain.DTOS.Product.Request;
using Products.Domain.DTOS.Product.Response;
using Products.Domain.DTOS.ProductGroups.Request;
using Products.Domain.DTOS.ProductGroups.Response;
using Products.Domain.Interfaces.IServices;

namespace products_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductGroupController(IProductGroupService<int> productService) : ControllerBase
{
    [HttpGet("get-groups")]
    public async Task<IActionResult> GetGroups(int? pageNumber,int? pageSize,string? sortBy,string? sortOrder,
        CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<GroupResponse> list;
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
            return Ok(new ListResult<GroupResponse>
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
    
    [HttpPost("add-group")]
    public async Task<IActionResult> Add([FromBody]GroupDto dto,CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await productService.Add(dto,cancellationToken));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("edit-group/{id:int}")]
    public async Task<IActionResult> Edit([FromBody]GroupDto dto,int id,CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await productService.Update(dto,id,cancellationToken));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("delete-groups")]
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
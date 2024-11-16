using Microsoft.EntityFrameworkCore;
using Products.Domain.Common;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IRepositories;
using Products.Infrastructure.Data;

namespace Products.Infrastructure.Repositories;

public class GroupsRepository(ApplicationDbContext dbContext) : IGroupsRepository
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Create(ProductsGroup entity, CancellationToken cancellationToken = default)
    {
        entity.Id = 0;
        await dbContext.ProductsGroups.AddAsync(entity,cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<ProductsGroup> GetById(int key, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.ProductsGroups.FirstOrDefaultAsync(e => e.Id == key,cancellationToken);
        if (entity is null)
            throw new Exception("No Products Group With This ID !");
        return entity;
    }

    public async Task Update(ProductsGroup entity, int key, CancellationToken cancellationToken = default)
    {
        var existingGroup = await dbContext.ProductsGroups.FirstOrDefaultAsync(e => e.Id == key, cancellationToken);
        if (existingGroup == null)
            throw new KeyNotFoundException($"Products Group with ID {key} not found.");
        dbContext.Entry(existingGroup).CurrentValues.SetValues(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(int key, CancellationToken cancellationToken = default)
    {
        var existingGroup = await dbContext.ProductsGroups.FirstOrDefaultAsync(e => e.Id == key, cancellationToken);
        if (existingGroup == null)
            throw new KeyNotFoundException($"Product with ID {key} not found.");
        try
        {
            dbContext.ProductsGroups.Remove(existingGroup);
            await SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            dbContext.Entry(existingGroup).State = EntityState.Unchanged;
            throw new Exception($"Error Delete Product With Key {key}");
        }
    }

    public async Task<IEnumerable<ProductsGroup>> GetList(CancellationToken cancellationToken = default)
    {
        return await dbContext.ProductsGroups.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductsGroup>> PaginatedList(PaginationParams paginationParams, CancellationToken cancellationToken = default)
    {
        var filters = paginationParams.Filters;
        var sortBy = paginationParams.SortBy;
        var sortOrder = paginationParams.SortOrder;
        
        var query = dbContext.ProductsGroups.AsNoTracking();
        
        //Filtering
        if (filters.Count != 0)
        {
            var expressionTree = ExpressionBuilder.ConstructAndExpressionTree<ProductsGroup>(filters);
            if(expressionTree is not null)
                query = query.Where(expressionTree);
        }
        
        //Sorting
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            var orderByExpression = ExpressionBuilder.GetOrderByExpression<ProductsGroup>(sortBy);
            switch (sortOrder)
            {
                case "asc" :
                    query = query.OrderBy(orderByExpression);
                    break;
                case "desc":
                    query = query.OrderByDescending(orderByExpression);
                    break;
            }
        }
        
        // Pagination
        query=  query
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize);
        
        return await query.ToListAsync(cancellationToken);
    }
}
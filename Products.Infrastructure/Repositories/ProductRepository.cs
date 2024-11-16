using Microsoft.EntityFrameworkCore;
using Products.Domain.Common;
using Products.Domain.Entities.Common;
using Products.Domain.Entities.Db;
using Products.Domain.Interfaces.IRepositories;
using Products.Infrastructure.Data;

namespace Products.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Create(Product entity, CancellationToken cancellationToken)
    {
        entity.Id = 0;
        await dbContext.Products.AddAsync(entity,cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<Product> GetById(int key, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Products.FirstOrDefaultAsync(e => e.Id == key,cancellationToken);
        if (entity is null)
            throw new Exception("No Product With This ID !");
        return entity;
    }

    public async Task Update(Product entity, int key, CancellationToken cancellationToken)
    {
        var existingProduct = await dbContext.Products.FirstOrDefaultAsync(e => e.Id == key, cancellationToken);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {key} not found.");
        }
        dbContext.Entry(existingProduct).CurrentValues.SetValues(entity);
        await SaveChangesAsync(cancellationToken);
    }
    
    public async Task Delete(int key, CancellationToken cancellationToken)
    {
        var existingProduct = await dbContext.Products.FirstOrDefaultAsync(e => e.Id == key, cancellationToken);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {key} not found.");
        }
        try
        {
            dbContext.Products.Remove(existingProduct);
            await SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            dbContext.Entry(existingProduct).State = EntityState.Unchanged;
            throw new Exception($"Error Delete Product With Key {key}");
        }
    }

    public async Task<IEnumerable<Product>> GetList(CancellationToken cancellationToken=default)
    {
        var list = await dbContext.Products.ToListAsync(cancellationToken);
        return list;
    }
    
    public async Task<IEnumerable<Product>> PaginatedList(PaginationParams paginationParams, CancellationToken cancellationToken=default)
    {
        var filters = paginationParams.Filters;
        var sortBy = paginationParams.SortBy;
        var sortOrder = paginationParams.SortOrder;
        
        var query = dbContext.Products.AsNoTracking();
        
        //Filtering
        if (filters.Count != 0)
        {
            var expressionTree = ExpressionBuilder.ConstructAndExpressionTree<Product>(filters);
            if(expressionTree is not null)
                query = query.Where(expressionTree);
        }
        
        //Sorting
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            var orderByExpression = ExpressionBuilder.GetOrderByExpression<Product>(sortBy);
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
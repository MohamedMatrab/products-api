using Products.Domain.Entities.Common;

namespace Products.Domain.Common;

public record PaginationParams
{
    public required int PageNumber { get; set; }
    public required int PageSize { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public List<ExpressionFilter> Filters { get; set; } = [];
}
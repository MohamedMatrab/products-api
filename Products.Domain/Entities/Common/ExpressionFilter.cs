using Products.Domain.Enums;

namespace Products.Domain.Entities.Common;

public class ExpressionFilter
{
    public string? PropertyName { get; set; }
    public object? Value { get; set; }
    public Comparison Comparison { get; set; }
}
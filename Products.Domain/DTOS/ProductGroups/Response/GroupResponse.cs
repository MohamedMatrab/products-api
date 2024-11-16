namespace Products.Domain.DTOS.ProductGroups.Response;

public record GroupResponse
{
    public int? Id { get; set; }
    public string Label { get; set; } = string.Empty;
}
namespace Products.Domain.DTOS.Product.Response;

public record ProductResponse
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
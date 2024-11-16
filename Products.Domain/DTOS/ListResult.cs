namespace Products.Domain.DTOS;

public record ListResult<TObject>
{
    public int PageNumber { get; set; }
    public int ResultsCount { get; set; }
    public IEnumerable<TObject> Results { get; set; } = [];
}
using System.ComponentModel.DataAnnotations;

namespace Products.Domain.Entities.Db;

public class ProductsGroup : Base<int>
{
    [MaxLength(100)]
    public string Label { get; set; }
    
    public List<Product> Products { get; set; }
}
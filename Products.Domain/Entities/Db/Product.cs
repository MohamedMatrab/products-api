using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Domain.Entities.Db;

public class Product : Base<int>
{
    [Required, StringLength(maximumLength: 8, MinimumLength = 2)]
    public string Code { get; set; }
    [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
    public string Name { get; set; }
    [Required]
    public double Price { get; set; }
    public int Quantity { get; set; }
    [StringLength(maximumLength: 350)]
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    
    public int? GroupId { get; set; }
    [ForeignKey(nameof(GroupId))]
    public ProductsGroup Group { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Products.Domain.Entities.Db;

public class Role : IdentityRole<int>
{
    [Required, StringLength(maximumLength: 10, MinimumLength = 2)]
    public string Code { get; set; }
    
    public int? EntryBy { get; set; }
    public DateTime? EntryDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
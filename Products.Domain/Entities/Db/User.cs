using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Products.Domain.Entities.Db;

public class User : IdentityUser<int>
{
    [StringLength(100)] public string FirstName { get; set; }
    [StringLength(100)] public string LastName { get; set; }
    
    public int? EntryBy { get; set; }
    public DateTime? EntryDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
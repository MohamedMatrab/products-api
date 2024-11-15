using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Entities.Db;

namespace Products.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User,Role,int>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductsGroup> ProductsGroups { get; set; }
}
using Microsoft.EntityFrameworkCore;
using products_api.Extensions;
using Products.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with SQL Server connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDb")));

// Register custom services and mappers
builder.Services.RegisterService();
builder.Services.RegisterMapperService();

// Add controllers and API-related services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization(); // Add this line to handle authorization middleware

app.MapControllers();

app.Run();
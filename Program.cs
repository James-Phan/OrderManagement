using Microsoft.EntityFrameworkCore;
using OrderManagement.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Đăng ký DbContext sử dụng SQL Server
builder.Services.AddDbContext<OrderManagement.Models.OrderManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository
builder.Services.AddScoped<OrderManagement.Repositories.ICustomerRepository, OrderManagement.Repositories.CustomerRepository>();
builder.Services.AddScoped<OrderManagement.Repositories.IProductRepository, OrderManagement.Repositories.ProductRepository>();
builder.Services.AddScoped<OrderManagement.Repositories.IOrderRepository, OrderManagement.Repositories.OrderRepository>();

// Service
builder.Services.AddScoped<OrderManagement.Services.ICustomerService, OrderManagement.Services.CustomerService>();
builder.Services.AddScoped<OrderManagement.Services.IProductService, OrderManagement.Services.ProductService>();
builder.Services.AddScoped<OrderManagement.Services.IOrderService, OrderManagement.Services.OrderService>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

var app = builder.Build();

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

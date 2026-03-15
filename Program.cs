using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "AllowSpecificOrigin";
var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins(allowedOrigins) // Allow requests from this origin
              .AllowAnyHeader()                  // Allow all headers
              .AllowAnyMethod();                 // Allow all HTTP methods (GET, POST, etc.)
    });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//Warning, if you change DB type you may need to go to the DB context and update the case insensitivity column settings
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<PagingSettings>(builder.Configuration.GetSection("PagingSettings"));

builder.Services.AddScoped<ICustomers, Customers>();
builder.Services.AddScoped<IDiscounts, Discounts>();
builder.Services.AddScoped<IProducts, Products>(); 
builder.Services.AddScoped<ISales, Sales>();
builder.Services.AddScoped<ISalesPersons, SalesPersons>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();

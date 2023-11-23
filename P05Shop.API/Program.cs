using P05Shop.API.Services.ProductService;
using P06.Shared.Services.ProductService;
using P06.Shared.Services.BookService;
using P05Shop.API.Services.BookService;
using Microsoft.EntityFrameworkCore;
using P05Shop.API.Models;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString)));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

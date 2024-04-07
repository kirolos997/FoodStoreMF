using FoodStore.Infrastrucutre.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<FoodStoreDbContext>(options =>
{
    string connectionString = builder.Configuration["ConnectionStrings:LocalHostDb"].ToString();

    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

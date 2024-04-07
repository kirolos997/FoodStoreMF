using FoodStore.API.Filters;
using FoodStore.API.Middelware;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.Services.Categories;
using FoodStore.Core.ServicesContracts.ICategories;
using FoodStore.Infrastrucutre.DBContext;
using FoodStore.Infrastrucutre.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<FoodStoreDbContext>(options =>
{
    string connectionString = builder.Configuration["ConnectionStrings:LocalHostDb"].ToString();

    options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<ValidateModelAttributes>();

builder.Services.AddScoped<ICategoriesRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoriesGetterService, CategoriesGetterService>();

builder.Services.AddScoped<ICategoriesUpdaterService, CategoriesUpdaterService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandlingMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

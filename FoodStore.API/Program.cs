using Asp.Versioning;
using FoodStore.API.Filters;
using FoodStore.API.Middelware;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.Services.Categories.v1;
using FoodStore.Core.Services.Products;
using FoodStore.Core.Services.Products.v1;
using FoodStore.Core.ServicesContracts.ICategories.v1;
using FoodStore.Core.ServicesContracts.IProducts;
using FoodStore.Core.ServicesContracts.IProducts.v1;
using FoodStore.Infrastrucutre.DBContext;
using FoodStore.Infrastrucutre.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
// Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});

builder.Services.AddControllers();

var apiVersioningBuilder = builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
    config.DefaultApiVersion = new ApiVersion(1);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

apiVersioningBuilder.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; //v1
    options.SubstituteApiVersionInUrl = true;
});

// Add services to the container.
builder.Services.AddDbContext<FoodStoreDbContext>(options =>
{
    string? connectionString = builder.Configuration["ConnectionStrings:LocalHostDb"]?.ToString();

    options.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties
    | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});

builder.Services.AddTransient<ControllerLogger>();
builder.Services.AddTransient<ActionLogger>();

builder.Services.AddScoped<ICategoriesRepository, CategoryRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

builder.Services.AddScoped<ICategoriesGetterService, CategoriesGetterService>();
builder.Services.AddScoped<FoodStore.Core.ServicesContracts.ICategories.v2.ICategoriesGetterService, FoodStore.Core.Services.Categories.v2.CategoriesGetterService>();
builder.Services.AddScoped<ICategoriesUpdaterService, CategoriesUpdaterService>();
builder.Services.AddScoped<ICategoriesDeleterService, CategoriesDeleterService>();
builder.Services.AddScoped<ICategoriesAdderService, CategoriesAdderService>();

builder.Services.AddScoped<IProductsGetterService, ProductsGetterService>();
builder.Services.AddScoped<IProductsDeleterService, ProductsDeleterService>();
builder.Services.AddScoped<IProductsUpdaterService, ProductsUpdaterService>();
builder.Services.AddScoped<IProductsAdderService, ProductsAdderService>();

if (builder.Environment.IsEnvironment("Test"))
{

}


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandlingMiddleware();

app.UseHttpLogging();

app.MapControllers();

app.Run();

public partial class Program { } // make the auto-generated program accessible programmatically
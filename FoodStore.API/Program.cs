using FoodStore.API.Filters;
using FoodStore.API.Middelware;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.Services.Categories;
using FoodStore.Core.ServicesContracts.ICategories;
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

// Add services to the container.
builder.Services.AddDbContext<FoodStoreDbContext>(options =>
{
    string connectionString = builder.Configuration["ConnectionStrings:LocalHostDb"].ToString();

    options.UseSqlServer(connectionString);
});

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties
    | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});

builder.Services.AddTransient<ValidateModelAttributes>();
builder.Services.AddTransient<ControllerLogger>();

builder.Services.AddScoped<ICategoriesRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoriesGetterService, CategoriesGetterService>();
builder.Services.AddScoped<ICategoriesUpdaterService, CategoriesUpdaterService>();
builder.Services.AddScoped<ICategoriesDeleterService, CategoriesDeleterService>();
builder.Services.AddScoped<ICategoriesAdderService, CategoriesAdderService>();



builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpLogging();

app.UseExceptionHandlingMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

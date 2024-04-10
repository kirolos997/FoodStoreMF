using FoodStore.Infrastrucutre.DBContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace FoodStore.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "IntegrationTest");

            builder.ConfigureServices(services =>
            {
                var descripter = services.SingleOrDefault(temp => temp.ServiceType == typeof(DbContextOptions<FoodStoreDbContext>));

                if (descripter != null)
                {
                    services.Remove(descripter);
                }

                services.AddDbContext<FoodStoreDbContext>(options =>
                {
                    options.UseInMemoryDatabase("DatbaseForTesting");
                });

            });
        }
    }
}

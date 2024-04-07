using FoodStore.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.Infrastrucutre.DBContext
{
    public class FoodStoreDbContext : DbContext
    {
        public virtual DbSet<Product> products { get; set; }
        public virtual DbSet<Category> categories { get; set; }

        public FoodStoreDbContext(DbContextOptions<FoodStoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Mapping the entities to real tables inside the database 
            modelBuilder.Entity<Product>().ToTable("Products");

            modelBuilder.Entity<Category>().ToTable("Categories");

            //Seed to Categories
            string categoriesJson = System.IO.File.ReadAllText("../FoodStore.Infrastrucutre/DBContext/_categories.json");
            List<Category>? categories = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(categoriesJson);

            foreach (Category item in categories)
                modelBuilder.Entity<Category>().HasData(item);


            //Seed to Products
            string productsJson = System.IO.File.ReadAllText("../FoodStore.Infrastrucutre/DBContext/_products.json");
            List<Product>? products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(productsJson);

            foreach (Product item in products)
                modelBuilder.Entity<Product>().HasData(item);

            // Prevent the cascade on delete for the products
            modelBuilder.Entity<Category>().HasOne<Product>().WithMany().OnDelete(DeleteBehavior.SetNull);

            // Calling OnModelCreating method from the DbContext class
            base.OnModelCreating(modelBuilder);

        }
    }
}

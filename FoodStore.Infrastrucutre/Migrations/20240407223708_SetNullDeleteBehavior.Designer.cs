﻿// <auto-generated />
using System;
using FoodStore.Infrastrucutre.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodStore.Infrastrucutre.Migrations
{
    [DbContext(typeof(FoodStoreDbContext))]
    [Migration("20240407223708_SetNullDeleteBehavior")]
    partial class SetNullDeleteBehavior
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodStore.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("Categories", (string)null);

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            Name = "Bakery"
                        },
                        new
                        {
                            CategoryId = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            Name = "Beverages"
                        },
                        new
                        {
                            CategoryId = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            Name = "Seafood"
                        },
                        new
                        {
                            CategoryId = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            Name = "Deli"
                        },
                        new
                        {
                            CategoryId = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            Name = "Snacks"
                        });
                });

            modelBuilder.Entity("FoodStore.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("InStore")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ProductDescription")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            ProductId = new Guid("c03bbe45-9aeb-4d24-99e0-4743016ffce9"),
                            CategoryId = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            InStore = true,
                            Price = 20.00m,
                            ProductDescription = "Sparkling water with lemon flavor",
                            ProductName = "Green Cola"
                        },
                        new
                        {
                            ProductId = new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928"),
                            CategoryId = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            InStore = true,
                            Price = 15.00m,
                            ProductDescription = "Sparkling water with orange flavor",
                            ProductName = "SpiroSpatis"
                        },
                        new
                        {
                            ProductId = new Guid("c6d50a47-f7e6-4482-8be0-4ddfc057fa6e"),
                            CategoryId = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            InStore = true,
                            Price = 5.00m,
                            ProductDescription = "Mineral water with low sodium",
                            ProductName = "Mineral water"
                        },
                        new
                        {
                            ProductId = new Guid("d15c6d9f-70b4-48c5-afd3-e71261f1a9be"),
                            CategoryId = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            InStore = true,
                            Price = 2.00m,
                            ProductDescription = "White bread with made from wheat flour",
                            ProductName = "White bread"
                        },
                        new
                        {
                            ProductId = new Guid("89e5f445-d89f-4e12-94e0-5ad5b235d704"),
                            CategoryId = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            InStore = true,
                            Price = 3.00m,
                            ProductDescription = "White bread with made from whole grains, seeds, and fiber-rich flour",
                            ProductName = "Diet bread"
                        },
                        new
                        {
                            ProductId = new Guid("2a6d3738-9def-43ac-9279-0310edc7ceca"),
                            CategoryId = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            InStore = true,
                            Price = 250.00m,
                            ProductDescription = "Shrimps from red sea",
                            ProductName = "Shrimps"
                        },
                        new
                        {
                            ProductId = new Guid("29339209-63f5-492f-8459-754943c74abf"),
                            CategoryId = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            InStore = true,
                            Price = 90.00m,
                            ProductDescription = "Sardines from red sea",
                            ProductName = "Sardines"
                        },
                        new
                        {
                            ProductId = new Guid("ac660a73-b0b7-4340-abc1-a914257a6189"),
                            CategoryId = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            InStore = true,
                            Price = 60.00m,
                            ProductDescription = "Nile tilapia from nile river",
                            ProductName = "Nile tilapia"
                        },
                        new
                        {
                            ProductId = new Guid("df660a73-c0b7-4340-abc1-a914257a5674"),
                            CategoryId = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            InStore = true,
                            Price = 350.00m,
                            ProductDescription = "Cheddar cheese with olives from Holland",
                            ProductName = "Cheddar cheese"
                        },
                        new
                        {
                            ProductId = new Guid("cb035f22-e7cf-4907-bd07-91cfee5240f3"),
                            CategoryId = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            InStore = true,
                            Price = 120.00m,
                            ProductDescription = "Low salt Feta cheese from Domty",
                            ProductName = "Feta cheese"
                        },
                        new
                        {
                            ProductId = new Guid("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"),
                            CategoryId = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            InStore = true,
                            Price = 160.00m,
                            ProductDescription = "Rich smoked turkey breast",
                            ProductName = "Turkey"
                        },
                        new
                        {
                            ProductId = new Guid("a3b9833b-8a4d-43e9-8690-61e08df81a9a"),
                            CategoryId = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            InStore = true,
                            Price = 130.00m,
                            ProductDescription = "Halwani Pastirma",
                            ProductName = "Pastirma"
                        },
                        new
                        {
                            ProductId = new Guid("1734ea16-2ae6-4c6a-a46e-641c6b9a8a62"),
                            CategoryId = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            InStore = true,
                            Price = 10.00m,
                            ProductDescription = "Chocolate Molto from edita",
                            ProductName = "Molto"
                        },
                        new
                        {
                            ProductId = new Guid("3eae53b3-1472-4465-b361-6703924fa6c6"),
                            CategoryId = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            InStore = true,
                            Price = 5.00m,
                            ProductDescription = "Abu Auf popcorn salted and caramel 100g",
                            ProductName = "Popcorn"
                        },
                        new
                        {
                            ProductId = new Guid("7be9243c-19ac-4b4c-8319-9211f99d4216"),
                            CategoryId = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            InStore = true,
                            Price = 10.25m,
                            ProductDescription = "Bake Rolz ketchup family size 90g",
                            ProductName = "Bake Rolz"
                        },
                        new
                        {
                            ProductId = new Guid("0b81647d-db2d-4d38-bac8-3cdbe38b8210"),
                            CategoryId = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            InStore = true,
                            Price = 5.00m,
                            ProductDescription = "Flaminco smoked cheese puffs 40g",
                            ProductName = "Falminco"
                        });
                });

            modelBuilder.Entity("FoodStore.Core.Entities.Category", b =>
                {
                    b.HasOne("FoodStore.Core.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("FoodStore.Core.Entities.Product", b =>
                {
                    b.HasOne("FoodStore.Core.Entities.Category", "Category")
                        .WithMany("products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FoodStore.Core.Entities.Category", b =>
                {
                    b.Navigation("products");
                });
#pragma warning restore 612, 618
        }
    }
}
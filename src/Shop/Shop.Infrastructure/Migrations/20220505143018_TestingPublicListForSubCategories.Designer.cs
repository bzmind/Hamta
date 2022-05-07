﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Infrastructure.Persistence.EF;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20220505143018_TestingPublicListForSubCategories")]
    partial class TestingPublicListForSubCategories
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence("CategoryHiLoSequence");

            modelBuilder.HasSequence("CommentHiLoSequence");

            modelBuilder.HasSequence("OrderHiLoSequence");

            modelBuilder.HasSequence("ProductHiLoSequence");

            modelBuilder.Entity("Shop.Domain.CategoryAggregate.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "CategoryHiLoSequence");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories", "category");
                });

            modelBuilder.Entity("Shop.Domain.ColorAggregate.Color", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("char(7)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Colors", "color");
                });

            modelBuilder.Entity("Shop.Domain.CommentAggregate.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "CommentHiLoSequence");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<int>("Dislikes")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("Recommendation")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Comments", "comment");
                });

            modelBuilder.Entity("Shop.Domain.CustomerAggregate.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("AvatarName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsSubscribedToNews")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Customers", "customer");
                });

            modelBuilder.Entity("Shop.Domain.InventoryAggregate.Inventory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("ColorId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DiscountPercentage")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDiscounted")
                        .HasColumnType("bit");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Inventories", "inventory");
                });

            modelBuilder.Entity("Shop.Domain.OrderAggregate.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "OrderHiLoSequence");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders", "order");
                });

            modelBuilder.Entity("Shop.Domain.ProductAggregate.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "ProductHiLoSequence");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("EnglishName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Products", "product");
                });

            modelBuilder.Entity("Shop.Domain.QuestionAggregate.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<long?>("ParentQuestionId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Questions", "question");
                });

            modelBuilder.Entity("Shop.Domain.ShippingAggregate.Shipping", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Method");

                    b.HasKey("Id");

                    b.ToTable("Shippings", "shipping");
                });

            modelBuilder.Entity("Shop.Domain.CategoryAggregate.Category", b =>
                {
                    b.HasOne("Shop.Domain.CategoryAggregate.Category", null)
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentId");

                    b.OwnsMany("Shop.Domain.CategoryAggregate.CategorySpecification", "Specifications", b1 =>
                        {
                            b1.Property<long>("CategoryId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("CategoryId", "Id");

                            b1.ToTable("Specifications", "category");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });

                    b.Navigation("Specifications");
                });

            modelBuilder.Entity("Shop.Domain.CommentAggregate.Comment", b =>
                {
                    b.OwnsMany("Shop.Domain.CommentAggregate.CommentHint", "CommentHints", b1 =>
                        {
                            b1.Property<long>("CommentId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Hint")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<int>("Status")
                                .HasMaxLength(10)
                                .HasColumnType("int");

                            b1.HasKey("CommentId", "Id");

                            b1.ToTable("Hints", "comment");

                            b1.WithOwner()
                                .HasForeignKey("CommentId");
                        });

                    b.Navigation("CommentHints");
                });

            modelBuilder.Entity("Shop.Domain.CustomerAggregate.Customer", b =>
                {
                    b.OwnsOne("Common.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<long>("CustomerId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("nvarchar(11)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers", "customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.OwnsMany("Shop.Domain.CustomerAggregate.CustomerAddress", "Addresses", b1 =>
                        {
                            b1.Property<long>("CustomerId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("FullAddress")
                                .IsRequired()
                                .HasMaxLength(300)
                                .HasColumnType("nvarchar(300)");

                            b1.Property<string>("FullName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<bool>("IsActive")
                                .HasColumnType("bit");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("CustomerId", "Id");

                            b1.ToTable("Addresses", "customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");

                            b1.OwnsOne("Common.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b2 =>
                                {
                                    b2.Property<long>("CustomerAddressCustomerId")
                                        .HasColumnType("bigint");

                                    b2.Property<long>("CustomerAddressId")
                                        .HasColumnType("bigint");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(11)
                                        .HasColumnType("nvarchar(11)")
                                        .HasColumnName("PhoneNumber");

                                    b2.HasKey("CustomerAddressCustomerId", "CustomerAddressId");

                                    b2.ToTable("Addresses", "customer");

                                    b2.WithOwner()
                                        .HasForeignKey("CustomerAddressCustomerId", "CustomerAddressId");
                                });

                            b1.Navigation("PhoneNumber")
                                .IsRequired();
                        });

                    b.OwnsMany("Shop.Domain.CustomerAggregate.CustomerFavoriteItem", "FavoriteItems", b1 =>
                        {
                            b1.Property<long>("CustomerId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<long>("ProductId")
                                .HasColumnType("bigint");

                            b1.HasKey("CustomerId", "Id");

                            b1.ToTable("FavoriteItems", "customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Addresses");

                    b.Navigation("FavoriteItems");

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("Shop.Domain.InventoryAggregate.Inventory", b =>
                {
                    b.OwnsOne("Common.Domain.ValueObjects.Money", "Price", b1 =>
                        {
                            b1.Property<long>("InventoryId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Price");

                            b1.HasKey("InventoryId");

                            b1.ToTable("Inventories", "inventory");

                            b1.WithOwner()
                                .HasForeignKey("InventoryId");
                        });

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("Shop.Domain.OrderAggregate.Order", b =>
                {
                    b.OwnsOne("Shop.Domain.OrderAggregate.OrderAddress", "Address", b1 =>
                        {
                            b1.Property<long>("OrderId")
                                .HasColumnType("bigint");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("FullAddress")
                                .IsRequired()
                                .HasMaxLength(300)
                                .HasColumnType("nvarchar(300)");

                            b1.Property<string>("FullName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("OrderId");

                            b1.ToTable("Addresses", "order");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.OwnsOne("Common.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b2 =>
                                {
                                    b2.Property<long>("OrderAddressOrderId")
                                        .HasColumnType("bigint");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(11)
                                        .HasColumnType("nvarchar(11)")
                                        .HasColumnName("PhoneNumber");

                                    b2.HasKey("OrderAddressOrderId");

                                    b2.ToTable("Addresses", "order");

                                    b2.WithOwner()
                                        .HasForeignKey("OrderAddressOrderId");
                                });

                            b1.Navigation("PhoneNumber")
                                .IsRequired();
                        });

                    b.OwnsMany("Shop.Domain.OrderAggregate.OrderItem", "Items", b1 =>
                        {
                            b1.Property<long>("OrderId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<int>("Count")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<long>("InventoryId")
                                .HasColumnType("bigint");

                            b1.HasKey("OrderId", "Id");

                            b1.ToTable("Items", "order");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.OwnsOne("Common.Domain.ValueObjects.Money", "Price", b2 =>
                                {
                                    b2.Property<long>("OrderItemOrderId")
                                        .HasColumnType("bigint");

                                    b2.Property<long>("OrderItemId")
                                        .HasColumnType("bigint");

                                    b2.Property<int>("Value")
                                        .HasColumnType("int")
                                        .HasColumnName("Price");

                                    b2.HasKey("OrderItemOrderId", "OrderItemId");

                                    b2.ToTable("Items", "order");

                                    b2.WithOwner()
                                        .HasForeignKey("OrderItemOrderId", "OrderItemId");
                                });

                            b1.Navigation("Price")
                                .IsRequired();
                        });

                    b.OwnsOne("Shop.Domain.OrderAggregate.ValueObjects.ShippingInfo", "ShippingInfo", b1 =>
                        {
                            b1.Property<long>("OrderId")
                                .HasColumnType("bigint");

                            b1.Property<string>("ShippingMethod")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("ShippingMethod");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders", "order");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.OwnsOne("Common.Domain.ValueObjects.Money", "ShippingCost", b2 =>
                                {
                                    b2.Property<long>("ShippingInfoOrderId")
                                        .HasColumnType("bigint");

                                    b2.Property<int>("Value")
                                        .HasColumnType("int")
                                        .HasColumnName("ShippingCost");

                                    b2.HasKey("ShippingInfoOrderId");

                                    b2.ToTable("Orders", "order");

                                    b2.WithOwner()
                                        .HasForeignKey("ShippingInfoOrderId");
                                });

                            b1.Navigation("ShippingCost")
                                .IsRequired();
                        });

                    b.Navigation("Address");

                    b.Navigation("Items");

                    b.Navigation("ShippingInfo");
                });

            modelBuilder.Entity("Shop.Domain.ProductAggregate.Product", b =>
                {
                    b.OwnsMany("Shop.Domain.ProductAggregate.ProductImage", "GalleryImages", b1 =>
                        {
                            b1.Property<long>("ProductId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("ProductId", "Id");

                            b1.ToTable("GalleryImages", "product");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Shop.Domain.ProductAggregate.ProductImage", "MainImage", b1 =>
                        {
                            b1.Property<long>("ProductId")
                                .HasColumnType("bigint");

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Images", "product");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsMany("Shop.Domain.ProductAggregate.ProductExtraDescription", "ExtraDescriptions", b1 =>
                        {
                            b1.Property<long>("ProductId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("ProductId", "Id");

                            b1.ToTable("ExtraDescriptions", "product");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsMany("Shop.Domain.ProductAggregate.ProductSpecification", "CustomSpecifications", b1 =>
                        {
                            b1.Property<long>("ProductId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<bool>("IsImportantFeature")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bit")
                                .HasDefaultValue(false);

                            b1.Property<string>("Key")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(300)
                                .HasColumnType("nvarchar(300)");

                            b1.HasKey("ProductId", "Id");

                            b1.ToTable("CustomSpecifications", "product");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsMany("Shop.Domain.ProductAggregate.Value_Objects.Score", "Scores", b1 =>
                        {
                            b1.Property<long>("ProductId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<int>("Value")
                                .HasMaxLength(5)
                                .HasColumnType("int");

                            b1.HasKey("ProductId", "Id");

                            b1.ToTable("Scores", "product");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("CustomSpecifications");

                    b.Navigation("ExtraDescriptions");

                    b.Navigation("GalleryImages");

                    b.Navigation("MainImage")
                        .IsRequired();

                    b.Navigation("Scores");
                });

            modelBuilder.Entity("Shop.Domain.ShippingAggregate.Shipping", b =>
                {
                    b.OwnsOne("Common.Domain.ValueObjects.Money", "Cost", b1 =>
                        {
                            b1.Property<long>("ShippingId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Cost");

                            b1.HasKey("ShippingId");

                            b1.ToTable("Shippings", "shipping");

                            b1.WithOwner()
                                .HasForeignKey("ShippingId");
                        });

                    b.Navigation("Cost")
                        .IsRequired();
                });

            modelBuilder.Entity("Shop.Domain.CategoryAggregate.Category", b =>
                {
                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}

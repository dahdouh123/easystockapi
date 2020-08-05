﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartRestaurant.Infrastructure.Persistence;

namespace SmartRestaurant.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartRestaurant.Domain.Entities.FoodBusiness", b =>
                {
                    b.Property<Guid>("FoodBusinessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AcceptTakeout")
                        .HasColumnType("bit");

                    b.Property<bool>("AcceptsCreditCards")
                        .HasColumnType("bit");

                    b.Property<double>("AverageRating")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FoodBusinessAdministratorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FoodBusinessCategory")
                        .HasColumnType("int");

                    b.Property<int>("FoodBusinessState")
                        .HasColumnType("int");

                    b.Property<bool>("HasCarParking")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHandicapFriendly")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NIF")
                        .HasColumnType("int");

                    b.Property<int>("NIS")
                        .HasColumnType("int");

                    b.Property<int>("NRC")
                        .HasColumnType("int");

                    b.Property<string>("NameArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameChinese")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameFrench")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRussian")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameSpanish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameTurkish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberRatings")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FoodBusinessId");

                    b.ToTable("FoodBusinesses");
                });

            modelBuilder.Entity("SmartRestaurant.Domain.Entities.FoodBusinessImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FoodBusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ImageBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLogo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FoodBusinessId");

                    b.ToTable("FoodBusinessImages");
                });

            modelBuilder.Entity("SmartRestaurant.Domain.Entities.FoodBusinessUser", b =>
                {
                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("FoodBusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ApplicationUserId", "FoodBusinessId");

                    b.HasIndex("FoodBusinessId");

                    b.ToTable("FoodBusinessUsers");
                });

            modelBuilder.Entity("SmartRestaurant.Domain.Entities.Zone", b =>
                {
                    b.Property<Guid>("ZoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FoodBusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZoneTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ZoneId");

                    b.HasIndex("FoodBusinessId");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("SmartRestaurant.Domain.Entities.FoodBusiness", b =>
                {
                    b.OwnsOne("SmartRestaurant.Domain.Entities.Globalisation.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("FoodBusinessId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StreetAddress")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("FoodBusinessId");

                            b1.ToTable("FoodBusinesses");

                            b1.WithOwner()
                                .HasForeignKey("FoodBusinessId");

                            b1.OwnsOne("SmartRestaurant.Domain.ValueObjects.GeoPosition", "GeoPosition", b2 =>
                                {
                                    b2.Property<Guid>("AddressFoodBusinessId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Latitude")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Longitude")
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("AddressFoodBusinessId");

                                    b2.ToTable("FoodBusinesses");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressFoodBusinessId");
                                });
                        });

                    b.OwnsOne("SmartRestaurant.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("FoodBusinessId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("CountryCode")
                                .HasColumnType("int");

                            b1.Property<int>("Number")
                                .HasColumnType("int");

                            b1.HasKey("FoodBusinessId");

                            b1.ToTable("FoodBusinesses");

                            b1.WithOwner()
                                .HasForeignKey("FoodBusinessId");
                        });
                });

            modelBuilder.Entity("SmartRestaurant.Domain.Entities.FoodBusinessImage", b =>
                {
                    b.HasOne("SmartRestaurant.Domain.Entities.FoodBusiness", "FoodBusiness")
                        .WithMany()
                        .HasForeignKey("FoodBusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartRestaurant.Domain.Entities.FoodBusinessUser", b =>
                {
                    b.HasOne("SmartRestaurant.Domain.Entities.FoodBusiness", "FoodBusiness")
                        .WithMany()
                        .HasForeignKey("FoodBusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartRestaurant.Domain.Entities.Zone", b =>
                {
                    b.HasOne("SmartRestaurant.Domain.Entities.FoodBusiness", "FoodBusiness")
                        .WithMany()
                        .HasForeignKey("FoodBusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

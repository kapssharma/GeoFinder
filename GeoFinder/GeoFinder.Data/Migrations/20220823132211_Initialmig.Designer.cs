﻿// <auto-generated />
using System;
using GeoFinder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeoFinder.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220823132211_Initialmig")]
    partial class Initialmig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GeoFinder.Model.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("StateId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("GeoFinder.Model.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("GeoFinder.Model.Roles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("GeoFinder.Model.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("GeoFinder.Model.TokenType", b =>
                {
                    b.Property<Guid>("TokenTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Token_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TokenTypeID");

                    b.ToTable("TokenType");
                });

            modelBuilder.Entity("GeoFinder.Model.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GeoFinder.Model.UserToken", b =>
                {
                    b.Property<Guid>("TokenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedByUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifyByUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifyOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TokenTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TokenID");

                    b.HasIndex("CreatedByUserID");

                    b.HasIndex("ModifyByUserID");

                    b.HasIndex("TokenTypeID");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("GeoFinder.Model.Address", b =>
                {
                    b.HasOne("GeoFinder.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeoFinder.Model.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("State");
                });

            modelBuilder.Entity("GeoFinder.Model.Roles", b =>
                {
                    b.HasOne("GeoFinder.Model.Users", "Createdbyuser")
                        .WithOne("Roles")
                        .HasForeignKey("GeoFinder.Model.Roles", "CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Createdbyuser");
                });

            modelBuilder.Entity("GeoFinder.Model.State", b =>
                {
                    b.HasOne("GeoFinder.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("GeoFinder.Model.Users", b =>
                {
                    b.HasOne("GeoFinder.Model.Address", "Address")
                        .WithOne("Createdbyuser")
                        .HasForeignKey("GeoFinder.Model.Users", "AddressId");

                    b.HasOne("GeoFinder.Model.Users", "Createdbyuser")
                        .WithOne("Modifiedbyuser")
                        .HasForeignKey("GeoFinder.Model.Users", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Createdbyuser");
                });

            modelBuilder.Entity("GeoFinder.Model.UserToken", b =>
                {
                    b.HasOne("GeoFinder.Model.Users", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserID");

                    b.HasOne("GeoFinder.Model.Users", "ModifyByUser")
                        .WithMany()
                        .HasForeignKey("ModifyByUserID");

                    b.HasOne("GeoFinder.Model.TokenType", "TokenType")
                        .WithMany()
                        .HasForeignKey("TokenTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeoFinder.Model.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("CreatedByUser");

                    b.Navigation("ModifyByUser");

                    b.Navigation("TokenType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GeoFinder.Model.Address", b =>
                {
                    b.Navigation("Createdbyuser")
                        .IsRequired();
                });

            modelBuilder.Entity("GeoFinder.Model.Users", b =>
                {
                    b.Navigation("Modifiedbyuser");

                    b.Navigation("Roles")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

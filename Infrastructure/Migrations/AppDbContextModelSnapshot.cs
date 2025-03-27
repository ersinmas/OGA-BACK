﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("IComments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.Trailer", b =>
                {
                    b.Property<int>("TrailerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrailerId"));

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("IComments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxWeight")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RegNumber")
                        .HasColumnType("int");

                    b.Property<string>("UComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrailerId");

                    b.ToTable("Trailers");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("IComments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("IComments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.VehicleTrailer", b =>
                {
                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.Property<int>("TrailerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BegDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("IComments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleId", "TrailerId", "BegDate", "EndDate");

                    b.HasIndex("TrailerId");

                    b.ToTable("VehicleTrailers");
                });

            modelBuilder.Entity("Domain.Entities.VehicleType", b =>
                {
                    b.Property<int>("VehicleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleTypeId"));

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("IComments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleTypeId");

                    b.ToTable("VehicleTypes");
                });

            modelBuilder.Entity("Domain.Entities.Vehicles", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("IComments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RegNumber")
                        .HasColumnType("int");

                    b.Property<string>("UComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleTypeId")
                        .HasColumnType("int");

                    b.HasKey("VehicleId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.VehicleTrailer", b =>
                {
                    b.HasOne("Domain.Entities.Trailer", "Trailer")
                        .WithMany("VehicleTrailer")
                        .HasForeignKey("TrailerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Vehicles", "Vehicles")
                        .WithMany("VehicleTrailer")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trailer");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Domain.Entities.Vehicles", b =>
                {
                    b.HasOne("Domain.Entities.VehicleType", "VehicleType")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.Trailer", b =>
                {
                    b.Navigation("VehicleTrailer");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.VehicleType", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Domain.Entities.Vehicles", b =>
                {
                    b.Navigation("VehicleTrailer");
                });
#pragma warning restore 612, 618
        }
    }
}

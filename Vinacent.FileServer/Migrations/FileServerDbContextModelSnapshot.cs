﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vinacent.FileServer.Data;

#nullable disable

namespace Vinacent.FileServer.Migrations
{
    [DbContext(typeof(FileServerDbContext))]
    partial class FileServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vinacent.FileServer.Data.Models.FileItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtensionJsonData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FileItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastUpdaterId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhysicalServerPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RootProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FileItemId");

                    b.HasIndex("RootProjectId");

                    b.ToTable("FileItems");
                });

            modelBuilder.Entity("Vinacent.FileServer.Data.Models.FileItemLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrowserInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FileItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FileItemLogs");
                });

            modelBuilder.Entity("Vinacent.FileServer.Data.Models.RootProject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RootProjects");
                });

            modelBuilder.Entity("Vinacent.FileServer.Data.Models.FileItem", b =>
                {
                    b.HasOne("Vinacent.FileServer.Data.Models.FileItem", null)
                        .WithMany("FileItems")
                        .HasForeignKey("FileItemId");

                    b.HasOne("Vinacent.FileServer.Data.Models.RootProject", "RootProject")
                        .WithMany()
                        .HasForeignKey("RootProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RootProject");
                });

            modelBuilder.Entity("Vinacent.FileServer.Data.Models.FileItem", b =>
                {
                    b.Navigation("FileItems");
                });
#pragma warning restore 612, 618
        }
    }
}
﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerAppNetworkForPhotographers.Models.Contexts;

#nullable disable

namespace ServerAppNetworkForPhotographers.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230603190031_UpdateFieldsForPhotographerAndComplaint")]
    partial class UpdateFieldsForPhotographerAndComplaint
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CategoryContent", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("ContentsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "ContentsId");

                    b.HasIndex("ContentsId");

                    b.ToTable("CategoryContent");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryDirId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryDirId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.CategoryDir", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryDirs");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PhotographerId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("PhotographerId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Complaint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ComplaintBaseId")
                        .HasColumnType("int");

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<int>("PhotographerId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ComplaintBaseId");

                    b.HasIndex("ContentId");

                    b.HasIndex("PhotographerId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.ComplaintBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ComplaintsBase");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BlogBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlogMainPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PhotographerId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PhotographerId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Favourite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<int>("PhotographerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("PhotographerId");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<int>("PhotographerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("PhotographerId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<string>("PhotoContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Photographer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoProfile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Photographers");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.PhotographerInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Awards")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhotographerId")
                        .HasColumnType("int");

                    b.Property<string>("Telegram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Viber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vk")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PhotographerId")
                        .IsUnique();

                    b.ToTable("PhotographersInfo");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.PhotoInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PhotoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId")
                        .IsUnique();

                    b.ToTable("PhotosInfo");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PhotographerId")
                        .HasColumnType("int");

                    b.Property<int>("SubscriberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PhotographerId");

                    b.HasIndex("SubscriberId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("CategoryContent", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Content", null)
                        .WithMany()
                        .HasForeignKey("ContentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Category", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.CategoryDir", "CategoryDir")
                        .WithMany("Categories")
                        .HasForeignKey("CategoryDirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryDir");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Comment", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Content", "Content")
                        .WithMany("Comments")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Photographer", "Photographer")
                        .WithMany("Comments")
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Complaint", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.ComplaintBase", "ComplaintBase")
                        .WithMany("Complaints")
                        .HasForeignKey("ComplaintBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Content", "Content")
                        .WithMany("Complaints")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Photographer", "Photographer")
                        .WithMany("Complaints")
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ComplaintBase");

                    b.Navigation("Content");

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Content", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Photographer", "Photographer")
                        .WithMany("Contents")
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Favourite", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Content", "Content")
                        .WithMany("Favourites")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Photographer", "Photographer")
                        .WithMany("Favourites")
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Like", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Content", "Content")
                        .WithMany("Likes")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Photographer", "Photographer")
                        .WithMany("Likes")
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Photo", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Content", "Content")
                        .WithMany("Photos")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.PhotographerInfo", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Photographer", "Photographer")
                        .WithOne("PhotographerInfo")
                        .HasForeignKey("ServerAppNetworkForPhotographers.Models.Data.PhotographerInfo", "PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.PhotoInfo", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Photo", "Photo")
                        .WithOne("PhotoInfo")
                        .HasForeignKey("ServerAppNetworkForPhotographers.Models.Data.PhotoInfo", "PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Subscription", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Photographer", "Photographer")
                        .WithMany()
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Data.Photographer", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photographer");

                    b.Navigation("Subscriber");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.CategoryDir", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.ComplaintBase", b =>
                {
                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Content", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Complaints");

                    b.Navigation("Favourites");

                    b.Navigation("Likes");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Photo", b =>
                {
                    b.Navigation("PhotoInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Data.Photographer", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Complaints");

                    b.Navigation("Contents");

                    b.Navigation("Favourites");

                    b.Navigation("Likes");

                    b.Navigation("PhotographerInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

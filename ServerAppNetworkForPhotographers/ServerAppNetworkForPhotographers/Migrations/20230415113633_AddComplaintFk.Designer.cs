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
    [Migration("20230415113633_AddComplaintFk")]
    partial class AddComplaintFk
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

            modelBuilder.Entity("ComplaintContent", b =>
                {
                    b.Property<int>("ComplaintsId")
                        .HasColumnType("int");

                    b.Property<int>("ContentsId")
                        .HasColumnType("int");

                    b.HasKey("ComplaintsId", "ContentsId");

                    b.HasIndex("ContentsId");

                    b.ToTable("ComplaintContent");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Category", b =>
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

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.CategoryDir", b =>
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

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
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

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Complaint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BlogBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlogPathMainPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PhotographerId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Like", b =>
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

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<string>("PathPhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Photographer", b =>
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

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PathProfilePhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Photographers");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.PhotographerInfo", b =>
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

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.PhotoInfo", b =>
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

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Subscription", b =>
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
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Content", null)
                        .WithMany()
                        .HasForeignKey("ContentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ComplaintContent", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Complaint", null)
                        .WithMany()
                        .HasForeignKey("ComplaintsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Content", null)
                        .WithMany()
                        .HasForeignKey("ContentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Category", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.CategoryDir", "CategoryDir")
                        .WithMany("Categories")
                        .HasForeignKey("CategoryDirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryDir");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Comment", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Content", "Content")
                        .WithMany("Comments")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Photographer", "Photographer")
                        .WithMany("Comments")
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Content", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Photographer", "Photographer")
                        .WithMany("Contents")
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Like", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Content", "Content")
                        .WithMany("Likes")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Photographer", "Photographer")
                        .WithMany("Likes")
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Photo", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Content", "Content")
                        .WithMany("Photos")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.PhotographerInfo", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Photographer", "Photographer")
                        .WithOne("PhotographerInfo")
                        .HasForeignKey("ServerAppNetworkForPhotographers.Models.PhotographerInfo", "PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.PhotoInfo", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Photo", "Photo")
                        .WithOne("PhotoInfo")
                        .HasForeignKey("ServerAppNetworkForPhotographers.Models.PhotoInfo", "PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Subscription", b =>
                {
                    b.HasOne("ServerAppNetworkForPhotographers.Models.Photographer", "Photographer")
                        .WithMany()
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerAppNetworkForPhotographers.Models.Photographer", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photographer");

                    b.Navigation("Subscriber");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.CategoryDir", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Content", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Photo", b =>
                {
                    b.Navigation("PhotoInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("ServerAppNetworkForPhotographers.Models.Photographer", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Contents");

                    b.Navigation("Likes");

                    b.Navigation("PhotographerInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

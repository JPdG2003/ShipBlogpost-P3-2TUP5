﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpottingBlogpost.Data;

#nullable disable

namespace SpottingBlogpost.Migrations
{
    [DbContext(typeof(SpottingContext))]
    partial class SpottingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.24");

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CommentType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PosterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShipId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PosterId");

                    b.HasIndex("ShipId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.Ship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Flag")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SpotterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SpotterId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("User");
                });

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.Member", b =>
                {
                    b.HasBaseType("SpottingBlogpost.Data.Entities.User");

                    b.HasDiscriminator().HasValue("Member");
                });

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.Spotter", b =>
                {
                    b.HasBaseType("SpottingBlogpost.Data.Entities.User");

                    b.HasDiscriminator().HasValue("Spotter");
                });

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.Admin", b =>
                {
                    b.HasBaseType("SpottingBlogpost.Data.Entities.Spotter");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.Comment", b =>
                {
                    b.HasOne("SpottingBlogpost.Data.Entities.User", "Poster")
                        .WithMany("Comments")
                        .HasForeignKey("PosterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpottingBlogpost.Data.Entities.Ship", "CommentedShip")
                        .WithMany("Comments")
                        .HasForeignKey("ShipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommentedShip");

                    b.Navigation("Poster");
                });

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.Ship", b =>
                {
                    b.HasOne("SpottingBlogpost.Data.Entities.Spotter", "Spotter")
                        .WithMany("SpottedShips")
                        .HasForeignKey("SpotterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spotter");
                });

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.Ship", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.User", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("SpottingBlogpost.Data.Entities.Spotter", b =>
                {
                    b.Navigation("SpottedShips");
                });
#pragma warning restore 612, 618
        }
    }
}

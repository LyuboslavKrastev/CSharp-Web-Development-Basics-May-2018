﻿// <auto-generated />
using System;
using CustomWebServer.GameStoreApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomWebServer.GameStoreApplication.Data.Models.Migrations
{
    [DbContext(typeof(GameStoreDbContext))]
    partial class GameStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomWebServer.GameStoreApplication.Data.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<DateTime?>("ReleaseDate");

                    b.Property<double>("Size");

                    b.Property<string>("ThumbnailUrl");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Trailer")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("CustomWebServer.GameStoreApplication.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Name")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CustomWebServer.GameStoreApplication.Data.Models.UserGame", b =>
                {
                    b.Property<int>("GameId");

                    b.Property<int>("UserId");

                    b.HasKey("GameId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGame");
                });

            modelBuilder.Entity("CustomWebServer.GameStoreApplication.Data.Models.UserGame", b =>
                {
                    b.HasOne("CustomWebServer.GameStoreApplication.Data.Models.Game", "Game")
                        .WithMany("Users")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CustomWebServer.GameStoreApplication.Data.Models.User", "User")
                        .WithMany("Games")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using UndergroundSound.Models;

namespace UndergroundSound.Migrations
{
    [DbContext(typeof(AppIdentityDbContext))]
    [Migration("20190601164750_Entities2")]
    partial class Entities2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("UndergroundSound.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("BandId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("BandId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("UndergroundSound.Models.Band", b =>
                {
                    b.Property<int>("BandId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MusicGenreId");

                    b.HasKey("BandId");

                    b.HasIndex("MusicGenreId");

                    b.ToTable("Bands");
                });

            modelBuilder.Entity("UndergroundSound.Models.BandsRecord", b =>
                {
                    b.Property<int>("BandsRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BandId");

                    b.HasKey("BandsRecordId");

                    b.HasIndex("BandId");

                    b.ToTable("BandsRecords");
                });

            modelBuilder.Entity("UndergroundSound.Models.MusicGenre", b =>
                {
                    b.Property<int>("MusicGenreId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("MusicGenreId");

                    b.ToTable("MusicGenres");
                });

            modelBuilder.Entity("UndergroundSound.Models.Song", b =>
                {
                    b.Property<int>("SongId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BandsRecordId");

                    b.HasKey("SongId");

                    b.HasIndex("BandsRecordId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("UndergroundSound.Models.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BandId");

                    b.Property<int?>("BandsRecordId");

                    b.Property<int?>("SongId");

                    b.HasKey("VoteId");

                    b.HasIndex("BandId");

                    b.HasIndex("BandsRecordId");

                    b.HasIndex("SongId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("UndergroundSound.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("UndergroundSound.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UndergroundSound.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("UndergroundSound.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UndergroundSound.Models.AppUser", b =>
                {
                    b.HasOne("UndergroundSound.Models.Band", "Band")
                        .WithMany("AppUsers")
                        .HasForeignKey("BandId");
                });

            modelBuilder.Entity("UndergroundSound.Models.Band", b =>
                {
                    b.HasOne("UndergroundSound.Models.MusicGenre", "MusicGenre")
                        .WithMany("Bands")
                        .HasForeignKey("MusicGenreId");
                });

            modelBuilder.Entity("UndergroundSound.Models.BandsRecord", b =>
                {
                    b.HasOne("UndergroundSound.Models.Band", "Band")
                        .WithMany("BandsRecords")
                        .HasForeignKey("BandId");
                });

            modelBuilder.Entity("UndergroundSound.Models.Song", b =>
                {
                    b.HasOne("UndergroundSound.Models.BandsRecord", "BandsRecord")
                        .WithMany("Songs")
                        .HasForeignKey("BandsRecordId");
                });

            modelBuilder.Entity("UndergroundSound.Models.Vote", b =>
                {
                    b.HasOne("UndergroundSound.Models.Band", "Band")
                        .WithMany("Votes")
                        .HasForeignKey("BandId");

                    b.HasOne("UndergroundSound.Models.BandsRecord", "BandsRecord")
                        .WithMany("Votes")
                        .HasForeignKey("BandsRecordId");

                    b.HasOne("UndergroundSound.Models.Song", "Song")
                        .WithMany("Votes")
                        .HasForeignKey("SongId");
                });
#pragma warning restore 612, 618
        }
    }
}

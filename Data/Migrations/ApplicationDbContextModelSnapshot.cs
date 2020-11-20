﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StartupProject_Asp.NetCore_PostGRE.Data;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Role","Identity");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1ca1e782-4354-4e44-a3b0-b45a338e3c0d"),
                            ConcurrencyStamp = "60cf2804-3ee5-4991-88e7-d52ae2483ca3",
                            Description = "Super Admin",
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = new Guid("00c9a295-1af4-4bc4-a779-996f9cb20c5c"),
                            ConcurrencyStamp = "81ec1b4b-8d6c-4d27-8a99-f019d33b9e92",
                            Description = "Admin",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("f9c2885d-bc10-4f9e-93d5-1dd3526baee7"),
                            ConcurrencyStamp = "2189708c-ddb9-4279-baf5-6a1d00c7fa0c",
                            Description = "Auditor",
                            Name = "Auditor",
                            NormalizedName = "AUDITOR"
                        },
                        new
                        {
                            Id = new Guid("7e959f0c-344a-4915-82ec-1168ed64d1fc"),
                            ConcurrencyStamp = "473e5ee1-fc1f-425d-8acb-0de7f03727b3",
                            Description = "Team Member",
                            Name = "TeamMember",
                            NormalizedName = "TEAMMEMBER"
                        },
                        new
                        {
                            Id = new Guid("2fed82eb-5780-4ad8-9c97-7caeda13deb1"),
                            ConcurrencyStamp = "50ab33ac-1957-41ad-a20d-eaeb67d07862",
                            Description = "Basic Member",
                            Name = "BasicMember",
                            NormalizedName = "BASICMEMBER"
                        });
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RoleId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.ToTable("RoleClaim","Identity");
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("bytea");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<int>("UsernameChangeLimit")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("User","Identity");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2ae31b85-32f7-4ea5-94d2-2b2c1a72910b"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2c34722b-6536-4cdc-bf41-32db7a6b0708",
                            Email = "abrar@jahin.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ABRAR@JAHIN.COM",
                            NormalizedUserName = "ABRAR",
                            PasswordHash = "AQAAAAEAACcQAAAAEGXlPXebKZuwNat25KrlauOvt8rq4Kfxrl0xXUXERG0fn4/R9sCyoozSVNb3BaD8Sw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "637414614000595449_ce77466d-ff1e-432c-ba07-e4e9a43442b3",
                            TwoFactorEnabled = false,
                            UserName = "abrar",
                            UsernameChangeLimit = 10
                        });
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserClaim","Identity");
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("LoginIp")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserLogin","Identity");
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RoleId1")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId1");

                    b.ToTable("UserRole","Identity");
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.UserToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.HasIndex("UserId1");

                    b.ToTable("UserToken","Identity");
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.RoleClaim", b =>
                {
                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.Role", null)
                        .WithMany("Claims")
                        .HasForeignKey("RoleId1");
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.UserClaim", b =>
                {
                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.User", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.UserLogin", b =>
                {
                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.User", null)
                        .WithMany("Logins")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.UserRole", b =>
                {
                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.Role", null)
                        .WithMany("Users")
                        .HasForeignKey("RoleId1");

                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.UserToken", b =>
                {
                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity.User", null)
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId1");
                });
#pragma warning restore 612, 618
        }
    }
}

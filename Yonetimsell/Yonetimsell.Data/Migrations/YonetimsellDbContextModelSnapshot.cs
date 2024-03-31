﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yonetimsell.Data.Concrete.Contexts;

#nullable disable

namespace Yonetimsell.Data.Migrations
{
    [DbContext(typeof(YonetimsellDbContext))]
    partial class YonetimsellDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.17");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "9b2b08dc-dab6-4f87-98c8-3b476b762d83",
                            RoleId = "bc675182-88fc-4b87-b49c-e45d38201538"
                        },
                        new
                        {
                            UserId = "b25278ce-ec4f-44c2-a182-6e6739418927",
                            RoleId = "bc675182-88fc-4b87-b49c-e45d38201538"
                        },
                        new
                        {
                            UserId = "c0278b15-0424-4b80-b1d3-cb0eb87f2b57",
                            RoleId = "5db41b7b-8725-43af-9466-d80b4bdb6389"
                        },
                        new
                        {
                            UserId = "018ae35c-9264-4b3b-b2be-10d926828538",
                            RoleId = "5db41b7b-8725-43af-9466-d80b4bdb6389"
                        },
                        new
                        {
                            UserId = "e65d4c66-0666-47aa-a35b-60384576d5d1",
                            RoleId = "27b14d8d-5759-46d8-99f3-c64526bbd7df"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.Identity.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "bc675182-88fc-4b87-b49c-e45d38201538",
                            Description = "Süper Yönetici haklarını taşıyan rol",
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = "5db41b7b-8725-43af-9466-d80b4bdb6389",
                            Description = "Yönetici haklarını taşıyan rol",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "27b14d8d-5759-46d8-99f3-c64526bbd7df",
                            Description = "Kullanıcı haklarını taşıyan rol",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        });
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.Identity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "9b2b08dc-dab6-4f87-98c8-3b476b762d83",
                            AccessFailedCount = 0,
                            Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                            City = "İstanbul",
                            ConcurrencyStamp = "0d71df86-9071-44fd-bbb8-d83b5983c6c4",
                            DateOfBirth = new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "baransel@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Baransel",
                            Gender = 1,
                            LastName = "Bayir",
                            LockoutEnabled = false,
                            NormalizedEmail = "BARANSEL@GMAIL.COM",
                            NormalizedUserName = "BARANSEL",
                            PasswordHash = "AQAAAAIAAYagAAAAEICtOzAm1ggOdGNjbtCowwSwLqvoqwx6m5gu3aRFdPyw3LBLip5Jl6rJWy1rQjTH+g==",
                            PhoneNumber = "05387654321",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5d7e7a4b-5713-4079-8be3-c85fbb731f43",
                            TwoFactorEnabled = false,
                            UserName = "baransel"
                        },
                        new
                        {
                            Id = "b25278ce-ec4f-44c2-a182-6e6739418927",
                            AccessFailedCount = 0,
                            Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                            City = "İstanbul",
                            ConcurrencyStamp = "3f8b542c-b75b-4815-84c3-ee81f416747d",
                            DateOfBirth = new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "engin@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Engin Niyazi",
                            Gender = 1,
                            LastName = "Ergül",
                            LockoutEnabled = false,
                            NormalizedEmail = "ENGIN@GMAIL.COM",
                            NormalizedUserName = "ENGINNIYAZI",
                            PasswordHash = "AQAAAAIAAYagAAAAEM9+J6ZmaGdZKD15owGNnZQIO8/Zpy4heEsMs0fy613bvWjm/CwTgqdNA2DZJjzxcQ==",
                            PhoneNumber = "05987654321",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "54b45774-05db-48f3-a1c8-622eb40637d1",
                            TwoFactorEnabled = false,
                            UserName = "enginniyazi"
                        },
                        new
                        {
                            Id = "c0278b15-0424-4b80-b1d3-cb0eb87f2b57",
                            AccessFailedCount = 0,
                            Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                            City = "İstanbul",
                            ConcurrencyStamp = "58561dbd-523a-4ad2-9b5b-ce9add5893ad",
                            DateOfBirth = new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lebron@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "LeBron",
                            Gender = 1,
                            LastName = "James",
                            LockoutEnabled = false,
                            NormalizedEmail = "LEBRON@GMAIL.COM",
                            NormalizedUserName = "LEBRON",
                            PasswordHash = "AQAAAAIAAYagAAAAEJlZH9+3bP9FAz31R5ZErBgn90JdSA+NHPdHDPeEZQKCrGZ2zUG/FqcyfeEsYCJQTg==",
                            PhoneNumber = "05487654321",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "b44d570b-7637-4cbe-bf30-53a14297884a",
                            TwoFactorEnabled = false,
                            UserName = "lebron"
                        },
                        new
                        {
                            Id = "018ae35c-9264-4b3b-b2be-10d926828538",
                            AccessFailedCount = 0,
                            Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                            City = "İstanbul",
                            ConcurrencyStamp = "76b4561b-09e7-4470-8854-2cd7ea0e9745",
                            DateOfBirth = new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "admin",
                            Gender = 0,
                            LastName = "admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEOfEwItTu64NrD0rxBsRn1O2iiVq/wKw3Rw65GnUQ4XFIsLjo6NIkt4gAYwdijZfQg==",
                            PhoneNumber = "05587654321",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d64ca9d4-9525-4153-bd25-668baab03b8e",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = "e65d4c66-0666-47aa-a35b-60384576d5d1",
                            AccessFailedCount = 0,
                            Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                            City = "İstanbul",
                            ConcurrencyStamp = "57d41ca5-e11d-49c2-a645-6b2da8513dd7",
                            DateOfBirth = new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "customer@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "customer",
                            Gender = 0,
                            LastName = "customer",
                            LockoutEnabled = false,
                            NormalizedEmail = "CUSTOMER@GMAIL.COM",
                            NormalizedUserName = "CUSTOMER",
                            PasswordHash = "AQAAAAIAAYagAAAAEA1QdN6328pkTUfBDXSUCzd4qToZWmY8lcElOPnNb94Ae+09RGiVpNtF+SMbARsVJg==",
                            PhoneNumber = "05687654321",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f03ea4ef-6bb6-4b2f-9767-ea66e6fca1f7",
                            TwoFactorEnabled = false,
                            UserName = "customer"
                        });
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.PTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("PTasks", (string)null);
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Budget")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("date('now')");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("date('now')");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SubscriptionDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("SubscriptionPlan")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.TeamMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectRole")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("TeamMembers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.PTask", b =>
                {
                    b.HasOne("Yonetimsell.Entity.Concrete.Project", "Project")
                        .WithMany("PTasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.User", "User")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("UserId");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.Project", b =>
                {
                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.Subscription", b =>
                {
                    b.HasOne("Yonetimsell.Entity.Concrete.Project", null)
                        .WithMany("Subscriptions")
                        .HasForeignKey("ProjectId");

                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.User", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.TeamMember", b =>
                {
                    b.HasOne("Yonetimsell.Entity.Concrete.Project", "Project")
                        .WithMany("TeamMembers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.User", "User")
                        .WithMany("TeamMemberships")
                        .HasForeignKey("UserId");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.Identity.User", b =>
                {
                    b.Navigation("AssignedTasks");

                    b.Navigation("Subscriptions");

                    b.Navigation("TeamMemberships");
                });

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.Project", b =>
                {
                    b.Navigation("PTasks");

                    b.Navigation("Subscriptions");

                    b.Navigation("TeamMembers");
                });
#pragma warning restore 612, 618
        }
    }
}

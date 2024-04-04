﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yonetimsell.Data.Concrete.Contexts;

#nullable disable

namespace Yonetimsell.Data.Migrations
{
    [DbContext(typeof(YonetimsellDbContext))]
    [Migration("20240402100826_InitialDb")]
    partial class InitialDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            UserId = "90f06179-8af2-4ba9-8b84-3df5d5766357",
                            RoleId = "7c7d29de-8d22-4f92-915d-7ef6c472e430"
                        },
                        new
                        {
                            UserId = "4d52d55b-371d-4ec3-b2fb-9f895d95e4a2",
                            RoleId = "7c7d29de-8d22-4f92-915d-7ef6c472e430"
                        },
                        new
                        {
                            UserId = "2518d805-c210-438e-aa65-153042b511e8",
                            RoleId = "dd9c2a92-6336-4628-b8ba-2e0160b82c1d"
                        },
                        new
                        {
                            UserId = "96b52b4e-eb5b-4098-ac65-33512cd50864",
                            RoleId = "dd9c2a92-6336-4628-b8ba-2e0160b82c1d"
                        },
                        new
                        {
                            UserId = "8390fc3e-9dbc-4119-9e81-cbebe6741fd7",
                            RoleId = "b1b46821-c08f-4a4b-b2b3-ce1ae8c46567"
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

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.Friendship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReceiverUserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SenderUserId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Friendship");
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
                            Id = "7c7d29de-8d22-4f92-915d-7ef6c472e430",
                            Description = "Süper Yönetici haklarını taşıyan rol",
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = "dd9c2a92-6336-4628-b8ba-2e0160b82c1d",
                            Description = "Yönetici haklarını taşıyan rol",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "b1b46821-c08f-4a4b-b2b3-ce1ae8c46567",
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
                            Id = "90f06179-8af2-4ba9-8b84-3df5d5766357",
                            AccessFailedCount = 0,
                            Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                            City = "İstanbul",
                            ConcurrencyStamp = "84847b39-30f4-4c4b-844b-7c67343ef980",
                            DateOfBirth = new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "baransel@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Baransel",
                            Gender = 1,
                            LastName = "Bayir",
                            LockoutEnabled = false,
                            NormalizedEmail = "BARANSEL@GMAIL.COM",
                            NormalizedUserName = "BARANSEL",
                            PasswordHash = "AQAAAAIAAYagAAAAEItkFkQgh0bok5XFBjNzopLjpUCUbpSvsNvapeV/aQNqghvNbypDWFdm6gJTJR5fYA==",
                            PhoneNumber = "05387654321",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "6edfefc2-22a8-4376-84b7-32d002b4c742",
                            TwoFactorEnabled = false,
                            UserName = "baransel"
                        },
                        new
                        {
                            Id = "4d52d55b-371d-4ec3-b2fb-9f895d95e4a2",
                            AccessFailedCount = 0,
                            Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                            City = "İstanbul",
                            ConcurrencyStamp = "db8dadbe-56d4-4b71-a78b-0312412c3830",
                            DateOfBirth = new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "engin@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Engin Niyazi",
                            Gender = 1,
                            LastName = "Ergül",
                            LockoutEnabled = false,
                            NormalizedEmail = "ENGIN@GMAIL.COM",
                            NormalizedUserName = "ENGINNIYAZI",
                            PasswordHash = "AQAAAAIAAYagAAAAECf100JtRN5bKSMxLYqvTuemjbnKZeUfCn6y2p6XIyHooIv8CbDOdaVT+qsUjEOh5g==",
                            PhoneNumber = "05987654321",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "afcd1c4b-87a0-42df-b24e-4b7468fcb43c",
                            TwoFactorEnabled = false,
                            UserName = "enginniyazi"
                        },
                        new
                        {
                            Id = "2518d805-c210-438e-aa65-153042b511e8",
                            AccessFailedCount = 0,
                            Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                            City = "İstanbul",
                            ConcurrencyStamp = "04955b7a-6a69-4900-8829-3455791c8516",
                            DateOfBirth = new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lebron@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "LeBron",
                            Gender = 1,
                            LastName = "James",
                            LockoutEnabled = false,
                            NormalizedEmail = "LEBRON@GMAIL.COM",
                            NormalizedUserName = "LEBRON",
                            PasswordHash = "AQAAAAIAAYagAAAAEBNDeEQnqmgTpkcfbp+xcYb3TquZ0u3DO2rwuX0lCmJLwJEGb4lBqLTuKHWnW9itQw==",
                            PhoneNumber = "05487654321",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c679ac49-ad12-4ff2-a110-dac83199188b",
                            TwoFactorEnabled = false,
                            UserName = "lebron"
                        },
                        new
                        {
                            Id = "96b52b4e-eb5b-4098-ac65-33512cd50864",
                            AccessFailedCount = 0,
                            Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                            City = "İstanbul",
                            ConcurrencyStamp = "d191d490-2721-4ec9-9447-0224bad093ac",
                            DateOfBirth = new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "admin",
                            Gender = 0,
                            LastName = "admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEALA5eP3hk1IWjlX2Ya3NC8lBKQfcJChTBzC6ZK4L7YNNOJbsP+kcb6ZewuQPBxJcQ==",
                            PhoneNumber = "05587654321",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "61e4aad3-fecc-4577-bae0-4e6be556f073",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = "8390fc3e-9dbc-4119-9e81-cbebe6741fd7",
                            AccessFailedCount = 0,
                            Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                            City = "İstanbul",
                            ConcurrencyStamp = "d2ee905c-22e6-4514-afcd-ef09a6cd5518",
                            DateOfBirth = new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "customer@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "customer",
                            Gender = 0,
                            LastName = "customer",
                            LockoutEnabled = false,
                            NormalizedEmail = "CUSTOMER@GMAIL.COM",
                            NormalizedUserName = "CUSTOMER",
                            PasswordHash = "AQAAAAIAAYagAAAAEBzRjZO7ZxnBAIJcKquf69pFTTVCesudmiP7Ie75LEWelkiAF/LCzY0AZrG9YhBEzQ==",
                            PhoneNumber = "05687654321",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9f392518-7af7-43db-b8d6-2dc371462f58",
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

            modelBuilder.Entity("Yonetimsell.Entity.Concrete.Friendship", b =>
                {
                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.User", "ReceiverUser")
                        .WithMany()
                        .HasForeignKey("ReceiverUserId");

                    b.HasOne("Yonetimsell.Entity.Concrete.Identity.User", "SenderUser")
                        .WithMany()
                        .HasForeignKey("SenderUserId");

                    b.Navigation("ReceiverUser");

                    b.Navigation("SenderUser");
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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;

namespace Yonetimsell.Data.Concrete.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            #region Roles

            List<Role> roles = new List<Role>
            {
                new Role{Name = "SuperAdmin", Description = "Süper Yönetici haklarını taşıyan rol", NormalizedName = "SUPERADMIN"},
                new Role{Name = "Admin", Description = "Yönetici haklarını taşıyan rol", NormalizedName = "ADMIN"},
                new Role{Name = "Customer", Description = "Kullanıcı haklarını taşıyan rol", NormalizedName = "CUSTOMER"}
            };
            modelBuilder.Entity<Role>().HasData(roles);
            #endregion
            #region Users
            List<User> users = new List<User>
            {
                new User
                {
                    FirstName = "Baransel",
                    LastName = "Bayir",
                    UserName = "baransel",
                    NormalizedUserName = "BARANSEL",
                    Email = "baransel@gmail.com",
                    NormalizedEmail = "BARANSEL@GMAIL.COM",
                    Gender = Gender.Man,
                    DateOfBirth = new DateTime(1998,4,20),
                    Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                    City = "İstanbul",
                    PhoneNumber = "05387654321",
                    EmailConfirmed = true
                },
                new User
                {
                    FirstName = "Engin Niyazi",
                    LastName = "Ergül",
                    UserName = "enginniyazi",
                    NormalizedUserName = "ENGINNIYAZI",
                    Email = "engin@gmail.com",
                    NormalizedEmail = "ENGIN@GMAIL.COM",
                    Gender = Gender.Man,
                    DateOfBirth = new DateTime(1998,4,20),
                    Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                    City = "İstanbul",
                    PhoneNumber = "05987654321",
                    EmailConfirmed = true
                },
                new User
                {
                    FirstName = "LeBron",
                    LastName = "James",
                    UserName = "lebron",
                    NormalizedUserName = "LEBRON",
                    Email = "lebron@gmail.com",
                    NormalizedEmail = "LEBRON@GMAIL.COM",
                    Gender = Gender.Man,
                    DateOfBirth = new DateTime(1998,4,20),
                    Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                    City = "İstanbul",
                    PhoneNumber = "05487654321",
                    EmailConfirmed = true
                },
                new User
                {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    Gender = Gender.Woman,
                    DateOfBirth = new DateTime(1998,4,20),
                    Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                    City = "İstanbul",
                    PhoneNumber = "05587654321",
                    EmailConfirmed = true
                },
                new User
                {
                    FirstName = "customer",
                    LastName = "customer",
                    UserName = "customer",
                    NormalizedUserName = "CUSTOMER",
                    Email = "customer@gmail.com",
                    NormalizedEmail = "CUSTOMER@GMAIL.COM",
                    Gender = Gender.Woman,
                    DateOfBirth = new DateTime(1998,4,20),
                    Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                    City = "İstanbul",
                    PhoneNumber = "05687654321",
                    EmailConfirmed = true
                }
            };
            modelBuilder.Entity<User>().HasData(users);
            #endregion
            #region Password
            var passwordHasher = new PasswordHasher<User>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Qwe123.");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "Qwe123.");
            users[2].PasswordHash = passwordHasher.HashPassword(users[2], "Qwe123.");
            users[3].PasswordHash = passwordHasher.HashPassword(users[3], "Qwe123.");
            users[4].PasswordHash = passwordHasher.HashPassword(users[4], "Qwe123.");
            #endregion
            #region Role Assignment
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id,
                    RoleId = roles[0].Id,
                },
                new IdentityUserRole<string>
                {
                    UserId = users[1].Id,
                    RoleId = roles[0].Id,
                },
                new IdentityUserRole<string>
                {
                    UserId = users[2].Id,
                    RoleId = roles[1].Id,
                },
                new IdentityUserRole<string>
                {
                    UserId = users[3].Id,
                    RoleId = roles[1].Id,
                },
                new IdentityUserRole<string>
                {
                    UserId = users[4].Id,
                    RoleId = roles[2].Id,
                }
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            #endregion
        }
    }
}

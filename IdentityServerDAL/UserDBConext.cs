using System;
using System.Collections.Generic;
using System.Text;
using IdentityServerModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace IdentityServerDAL
{
    public class UserDBConext : IdentityDbContext<EMUsers>
    {
        public UserDBConext(DbContextOptions<UserDBConext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.HasDefaultSchema("AspNetIdentity");
            UsersSeed(builder);
        }
        private void UsersSeed(ModelBuilder builder)
        {
            var password = "Pass@word1";

            var alice = new EMUsers
            {
                Id = "100",
                UserName = "alice",
                NormalizedUserName = "ALICE",
                Email = "AliceSmith@email.com",
                NormalizedEmail = "AliceSmith@email.com".ToUpper(),
                EmailConfirmed = true
            };
            alice.PasswordHash = new PasswordHasher<EMUsers>().HashPassword(alice, password);

            var bob = new EMUsers
            {
                Id = "101",
                UserName = "bob",
                NormalizedUserName = "BOB",
                Email = "BobSmith@email.com",
                NormalizedEmail = "bobsmith@email.com".ToUpper(),
                EmailConfirmed = true,
            };
            bob.PasswordHash = new PasswordHasher<EMUsers>().HashPassword(bob, password);

            builder.Entity<EMUsers>()
                .HasData(alice, bob);

            builder.Entity<IdentityUserClaim<string>>()
                .HasData(
                    new IdentityUserClaim<string>
                    {
                        Id = 100,
                        UserId = "100",
                        ClaimType = "name",
                        ClaimValue = "Alice Smith"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 101,
                        UserId = "100",
                        ClaimType = "given_name",
                        ClaimValue = "Alice"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 102,
                        UserId = "100",
                        ClaimType = "family_name",
                        ClaimValue = "Smith"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 103,
                        UserId = "100",
                        ClaimType = "email",
                        ClaimValue = "AliceSmith@email.com"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 104,
                        UserId = "100",
                        ClaimType = "website",
                        ClaimValue = "http://alice.com"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 105,
                        UserId = "101",
                        ClaimType = "name",
                        ClaimValue = "Bob Smith"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 106,
                        UserId = "101",
                        ClaimType = "given_name",
                        ClaimValue = "Bob"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 107,
                        UserId = "101",
                        ClaimType = "family_name",
                        ClaimValue = "Smith"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 108,
                        UserId = "101",
                        ClaimType = "email",
                        ClaimValue = "BobSmith@email.com"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 109,
                        UserId = "101",
                        ClaimType = "website",
                        ClaimValue = "http://bob.com"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 110,
                        UserId = "101",
                        ClaimType = "email_verified",
                        ClaimValue = true.ToString()
                    });
        }
    }
}

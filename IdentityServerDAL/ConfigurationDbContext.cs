using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using System;

namespace IdentityServerDAL
{
    public class ConfigurationDbContext : ConfigurationDbContext<ConfigurationDbContext>
    {
        private readonly ConfigurationStoreOptions _storeOptions;

        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
            _storeOptions = storeOptions;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureClientContext(_storeOptions);
            modelBuilder.ConfigureResourcesContext(_storeOptions);

            base.OnModelCreating(modelBuilder);

            ClientSeed(modelBuilder);
        }

        private void ClientSeed(ModelBuilder builder)
        {
            builder.Entity<ApiResource>()
                .HasData(
                    new ApiResource
                    {
                        Id = 1,
                        Name = "web_api",
                        DisplayName = "My Web API"
                    }
                );

            builder.Entity<ApiScope>()
                .HasData(
                    new ApiScope
                    {
                        Id = 1,
                        Name = "web_api",
                        DisplayName = "web_api",
                        Description = null,
                        Required = false,
                        Emphasize = false,
                        ShowInDiscoveryDocument = true,
                        ApiResourceId = 1
                    }
                );

            builder.Entity<IdentityResource>().HasData
                (
                    new IdentityResource()
                    {
                        Id = 1,
                        Enabled = true,
                        Name = "openid",
                        DisplayName = "Your user identifier",
                        Description = null,
                        Required = true,
                        Emphasize = false,
                        ShowInDiscoveryDocument = true,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        NonEditable = false
                    },
                    new IdentityResource()
                    {
                        Id = 2,
                        Enabled = true,
                        Name = "profile",
                        DisplayName = "User profile",
                        Description = "Your user profile information (first name, last name, etc.)",
                        Required = false,
                        Emphasize = true,
                        ShowInDiscoveryDocument = true,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        NonEditable = false
                    });

            builder.Entity<IdentityClaim>()
                .HasData(
                    new IdentityClaim
                    {
                        Id = 1,
                        IdentityResourceId = 1,
                        Type = "sub"
                    },
                    new IdentityClaim
                    {
                        Id = 2,
                        IdentityResourceId = 2,
                        Type = "email"
                    },
                    new IdentityClaim
                    {
                        Id = 3,
                        IdentityResourceId = 2,
                        Type = "website"
                    },
                    new IdentityClaim
                    {
                        Id = 4,
                        IdentityResourceId = 2,
                        Type = "given_name"
                    },
                    new IdentityClaim
                    {
                        Id = 5,
                        IdentityResourceId = 2,
                        Type = "family_name"
                    },
                    new IdentityClaim
                    {
                        Id = 6,
                        IdentityResourceId = 2,
                        Type = "name"
                    });

            builder.Entity<Client>()
                .HasData(                    
                    new Client
                    {
                        Id = 1,
                        Enabled = true,
                        ClientId = "EZM",
                        ProtocolType = "oidc",
                        RequireClientSecret = true,
                        RequireConsent = true,
                        ClientName = "EZM Client",
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = true,
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = true
                    });

            builder.Entity<ClientGrantType>()
                .HasData(
                    new ClientGrantType
                    {
                        Id = 1,
                        GrantType = "authorization_code",
                        ClientId = 1
                    });

            builder.Entity<ClientScope>()
                .HasData(
                    new ClientScope
                    {
                        Id = 1,
                        Scope = "profile",
                        ClientId = 1
                    }
                    ,
                    new ClientScope
                    {
                        Id = 2,
                        Scope = "openid",
                        ClientId = 1
                    },
                    new ClientScope
                    {
                        Id = 3,
                        Scope = "web_api",
                        ClientId = 1
                    });

            builder.Entity<ClientSecret>()
                .HasData(
                new ClientSecret
                {
                    Id = 1,
                    Value = "secret".ToSha256(),
                    Type = "SharedSecret",
                    ClientId = 1
                });

            builder.Entity<ClientPostLogoutRedirectUri>()
                .HasData(
                new ClientPostLogoutRedirectUri
                {
                    Id = 1,
                    PostLogoutRedirectUri = "http://localhost:5002/signout-callback-oidc",
                    ClientId = 1
                });

            builder.Entity<ClientRedirectUri>()
                .HasData(
                new ClientRedirectUri
                {
                    Id = 1,
                    RedirectUri = "http://localhost:5002/signin-oidc",
                    ClientId = 1
                });
        }
    }
}
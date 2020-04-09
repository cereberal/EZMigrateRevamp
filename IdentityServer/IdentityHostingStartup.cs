using System.Reflection;
using IdentityServerDAL;
using IdentityServerModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(IdentityServer.Areas.Identity.IdentityHostingStartup))]
namespace IdentityServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            var migrationsAssembly = typeof(UserDBConext).GetTypeInfo().Assembly.GetName().Name;

            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UserDBConext>(options =>
                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"),
                        sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddDefaultIdentity<EMUsers>(options => 
                    options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<UserDBConext>()
                    .AddDefaultTokenProviders();
            });
        }
    }
}
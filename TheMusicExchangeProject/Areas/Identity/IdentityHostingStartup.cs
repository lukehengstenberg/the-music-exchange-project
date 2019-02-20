using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheMusicExchangeProject.Areas.Identity.Data;
using TheMusicExchangeProject.Models;

[assembly: HostingStartup(typeof(TheMusicExchangeProject.Areas.Identity.IdentityHostingStartup))]
namespace TheMusicExchangeProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TheMusicExchangeProjectContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TheMusicExchangeProjectContextConnection")));

                services.AddDefaultIdentity<TheMusicExchangeProjectUser>()
                    .AddEntityFrameworkStores<TheMusicExchangeProjectContext>();
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Infrastracture.Persistence;
using Microsoft.AspNetCore.Identity;
using BlastAsia.DigiBook.Infrastructure.Security;
using Infrastracture.Persistence.Seeders;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            RunSeeder(host);

            host.Run();
        }

        private static void RunSeeder(IWebHost host)
        {
            var serviceScopeFactory = (IServiceScopeFactory)host.Services.GetRequiredService(typeof(IServiceScopeFactory));

            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApiDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
                Seeder.Seed(dbContext, userManager, roleManager);
            }

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

using Identity_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Autofac.Extensions.DependencyInjection;

namespace Identity_SimpleCloud_MicroservicesHttp.WebUI
{
    public static class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<IdentityDbContext>();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }

                    // var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    // await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager);
                    await IdentityDbContextSeed.SeedSampleDataAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                //.ConfigureLogging(logging =>
                //    {
                //        logging.ClearProviders();
                //        logging.AddConsole();
                //    })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

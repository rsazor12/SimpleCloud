using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Payment_SimpleCloud_MicroservicesMessageBroker.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace Payment_SimpleCloud_MicroservicesMessageBroker
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
                    var context = services.GetRequiredService<PaymentDbContext>();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }

                    // var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    // await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager);
                    await PaymentDbContextSeed.SeedSampleDataAsync(context);
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

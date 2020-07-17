using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Configurations;
using MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence;
using MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Services;
using ModelBuilder = MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Services.ModelBuilder;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure
{
    public static class DependencyInjection
    {
        // public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IOptions<AppSettings> appSettings)
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppSettings appSettings)
        {

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        // configuration.GetConnectionString("DefaultConnection"),
            //        appSettings.ConnectionStrings.DefaultConnection,
            //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddDbContext<MachineLearningDbContext>(options => options.UseSqlServer(
                appSettings.ConnectionStrings.DefaultConnection,
                b => b.MigrationsAssembly(typeof(MachineLearningDbContext).Assembly.FullName)));

            services.AddScoped<IMachineLearningDbContext>(provider => provider.GetService<MachineLearningDbContext>());

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            // services 
            services.AddTransient<IDateTime, DateTimeService>();
            //services.AddTransient<IIdentityService, IdentityService>();
            // services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddTransient<IModelBuilder, ModelBuilder>();

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            return services;
        }
    }
}

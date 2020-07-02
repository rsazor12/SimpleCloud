using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleCloud_Monolithic.Application.Common.Configurations;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using SimpleCloudMonolithic.Infrastructure.Files;
using SimpleCloudMonolithic.Infrastructure.Persistence;
using SimpleCloudMonolithic.Infrastructure.Services;
using ModelBuilder = SimpleCloudMonolithic.Infrastructure.Services.ModelBuilder;

namespace SimpleCloudMonolithic.Infrastructure
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

            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(
                appSettings.ConnectionStrings.DefaultConnection,
                b => b.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)));

            services.AddScoped<IIdentityDbContext>(provider => provider.GetService<IdentityDbContext>());

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            // services 
            services.AddTransient<IDateTime, DateTimeService>();
            //services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddTransient<IModelBuilder, ModelBuilder>();

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            return services;
        }
    }
}

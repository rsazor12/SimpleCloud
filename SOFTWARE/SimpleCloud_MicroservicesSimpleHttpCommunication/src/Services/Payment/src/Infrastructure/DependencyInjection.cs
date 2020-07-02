using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Configurations;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Payment_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence;
using Payment_SimpleCloud_MicroservicesHttp.Infrastructure.Services;

namespace Payment_SimpleCloud_MicroservicesHttp.Infrastructure
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

            services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(
                appSettings.ConnectionStrings.DefaultConnection,
                b => b.MigrationsAssembly(typeof(PaymentDbContext).Assembly.FullName)));

            services.AddScoped<IPaymentDbContext>(provider => provider.GetService<PaymentDbContext>());

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            // services 
            services.AddTransient<IDateTime, DateTimeService>();
            //services.AddTransient<IIdentityService, IdentityService>();
            //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            //services.AddTransient<IModelBuilder, ModelBuilder>();

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            return services;
        }
    }
}

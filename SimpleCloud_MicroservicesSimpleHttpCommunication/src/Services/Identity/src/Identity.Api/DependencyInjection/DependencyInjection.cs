using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Configurations;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using SimpleCloudMonolithic.Infrastructure.Persistence;
using SimpleCloudMonolithic.Infrastructure.Services;

namespace SimpleCloudMonolithic.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }

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

            // services 
            services.AddTransient<IDateTime, DateTimeService>();
            

            return services;
        }
    }
}

using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Identity_SimpleCloud_MicroservicesHttp.Infrastructure;
using Identity_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence;
using Identity_SimpleCloud_MicroservicesHttp.WebUI.Filters;
using Identity_SimpleCloud_MicroservicesHttp.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Configurations;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ;
using RabbitMQ.Client;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Autofac;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus;

namespace Identity_SimpleCloud_MicroservicesHttp.WebUI
{
    public class Startup
    {
        public AppSettings _appSettings;
        public Startup(
            IConfiguration configuration
            )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
    
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));

            _appSettings = new AppSettings();
            Configuration.GetSection(nameof(AppSettings)).Bind(_appSettings);

            AddEventBus(services);

            services.AddInfrastructure(_appSettings);


            services.AddApplication();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<IdentityDbContext>();

            services.AddControllersWithViews(options => 
                options.Filters.Add(new ApiExceptionFilter()));

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //// In production, the Angular files will be served from this directory
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist";
            //});

            services.AddOpenApiDocument();
                //configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                //{
                //    Type = OpenApiSecuritySchemeType.ApiKey,
                //    Name = "Authorization",
                //    In = OpenApiSecurityApiKeyLocation.Header,
                //    Description = "Type into the textbox: Bearer {your JWT token}."
                //});

                //configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
            //{
                //loggerFactory..AddConsole(Configuration.GetSection("Logging"));
                //loggerFactory.AddDebug();
                //removed the rest of the lines
            //}

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseRouting();

            //app.UseAuthentication();
            // app.UseIdentityServer();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            //app.UseSpa(spa =>
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501

            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
        }

        public void AddEventBus(IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                // var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = _appSettings.EventBusHostName,
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(_appSettings.EventBusUserName))
                {
                    factory.UserName = _appSettings.EventBusUserName;
                }

                if (!string.IsNullOrEmpty(_appSettings.EventBusPassword))
                {
                    factory.Password = _appSettings.EventBusPassword;
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(_appSettings.EventBusRetryCount))
                {
                    retryCount = int.Parse(_appSettings.EventBusRetryCount);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });


            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(_appSettings.EventBusRetryCount))
                {
                    retryCount = int.Parse(_appSettings.EventBusRetryCount);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, _appSettings.SubscriptionClientName, retryCount);
            });
        }


    }
}

using MassTransit;
using AutoMapper;
using BlazorApp.Data.EF;
using BlazorApp.DataAcess;
using BlazorApp.DataAcess.EF.Extensions;
using BlazorApp.DataAcess.Mapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using NLog.Config;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private ILogger<Startup> ILogger { get; set; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = GetLogger(loggerFactory);
            LogConfiguration(logger);

            services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = true; // optional
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();


            var connectionDB = _configuration["DefaultConnectionDB"];

            //ServiceLifetime.Transient
            //ServiceLifetime.Scoped
            //ServiceLifetime.Singleton
            
            //Connection IdentityUser
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(connectionDB), ServiceLifetime.Transient);

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            var connection = _configuration["DefaultConnection"];

            //Connection DB
            services.AddDbContext<Context>(options =>
                options.UseLazyLoadingProxies(false)
                   .UseSqlServer(connection), ServiceLifetime.Transient);

            ConfigurationItemFactory.Default.Targets.RegisterDefinition("ApplicationInsightsTarget", typeof(Microsoft.ApplicationInsights.NLogTarget.ApplicationInsightsTarget));

            services.AddApplicationInsightsTelemetry(options =>
            {
                options.InstrumentationKey = _configuration["ApplicationInsights:InstrumentationKey"];
                options.EnableAdaptiveSampling = false;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", c => c.RequireRole("Admin"));
            });

            // create the bus using Azure Service bus
            //var azureServiceBus = Bus.Factory.CreateUsingAzureServiceBus(busFactoryConfig =>
            //{
            //    busFactoryConfig.Host(_configuration["Masstransit:AzureServiceBusConnectionString"], hostConfig =>
            //    {
            //        // This is optional, but you can specify the protocol to use.
            //        hostConfig.TransportType = TransportType.AmqpWebSockets;
            //    });
            //});

            // Add MassTransit
            //services.AddMassTransit(config =>
            //{
            //    config.AddBus(provider => azureServiceBus);
            //});

            //services.AddSingleton<IPublishEndpoint>(azureServiceBus);
            //services.AddSingleton<ISendEndpointProvider>(azureServiceBus);
            //services.AddSingleton<IBus>(azureServiceBus);

            services.AddHttpClient();
            services.AddRazorPages();
            services.AddControllers();
            services.AddServerSideBlazor();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Pages");

            //Infraestructure services
            services.AddInfraestrutureServices(_configuration);

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContextOptions<ApplicationDbContext> identityDbContextOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            EnsureTestUsers(identityDbContextOptions, userManager, roleManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private ILogger<Startup> GetLogger(ILoggerFactory loggerFactory)
        {
            if (ILogger != null)
            {
                return ILogger;
            }

            ILogger = loggerFactory.CreateLogger<Startup>();

            return ILogger;
        }

        private void LogConfiguration(ILogger<Startup> logger)
        {
            var root = (IConfigurationRoot)_configuration;
            var debugView = root.GetDebugView();
            logger.LogInformation($"\n//////////////////////////////////////////////////////////////////////////////////////////////////\n//                                   Tracing Configuration                                      //\n//////////////////////////////////////////////////////////////////////////////////////////////////\n\n{debugView}\n\n//////////////////////////////////////////////////////////////////////////////////////////////////\n//                                          END                                                 //\n//////////////////////////////////////////////////////////////////////////////////////////////////");
        }
        private static void EnsureTestUsers(DbContextOptions<ApplicationDbContext> identityDbContextOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //create bda identity
            using (var db = new ApplicationDbContext(identityDbContextOptions))
            {
                db.Database.EnsureCreated();
            }
        }
    }
}

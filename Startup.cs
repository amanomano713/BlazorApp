//using MassTransit;
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
using Microsoft.AspNetCore.ResponseCompression;
using BlazorApp.Hubs;
using BlazorApp.Cache;
using EurofirmsGroup.Caching.Redis;


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


            services.AddResponseCompression(opts =>
             {
                 opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                     new[] { "application/octet-stream" });
             });

            services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = true; // optional
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();


            var connectionDB = _configuration["ConnectionStrings:DefaultConnectionDB"];

            //ServiceLifetime.Transient
            //ServiceLifetime.Scoped
            //ServiceLifetime.Singleton
            
            //Connection IdentityUser
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(connectionDB), ServiceLifetime.Singleton);

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            var connection = _configuration["ConnectionStrings:DefaultConnection"];

            //Connection DB
            services.AddDbContext<Context>(options =>
                options.UseLazyLoadingProxies(false)
                   .UseSqlServer(connection), ServiceLifetime.Transient);


            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", c => c.RequireRole("Admin"));
            });

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

            //services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            //{
            //    builder
            //        .AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader();
            //}));

            services.AddSingleton<ICacheService>(sp => new CacheService(_configuration));

            services.AddMemoryCache();

            services.AddSingleton<ICacheBase, CacheMemoryHelper>();

            //Microservice
            services.AddHttpClient("Api.Users", connec =>
            {
                connec.BaseAddress = new Uri("http://EC2Co-EcsEl-AXDTJMZ4B8SD-558231051.eu-west-2.elb.amazonaws.com/API/v1/Users/");
                connec.DefaultRequestHeaders.Add("Accept", "application/json");
                connec.DefaultRequestHeaders.Add("Accept-Language", Thread.CurrentThread.CurrentUICulture.Name);
            });

            services.AddHttpClient("Api.authenticate", connec =>
            {
                connec.BaseAddress = new Uri("http://EC2Co-EcsEl-AXDTJMZ4B8SD-558231051.eu-west-2.elb.amazonaws.com/");
                connec.DefaultRequestHeaders.Add("Accept", "application/json");
                connec.DefaultRequestHeaders.Add("Accept-Language", Thread.CurrentThread.CurrentUICulture.Name);
            });

            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContextOptions<ApplicationDbContext> identityDbContextOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //EnsureTestUsers(identityDbContextOptions, userManager, roleManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapHub<ChatHub>("/chathub");
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
            //using (var db = new ApplicationDbContext(identityDbContextOptions))
            //{
            //    db.Database.EnsureCreated();
            //}
        }
    }
}

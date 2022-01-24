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

namespace BlazorApp
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = true; // optional
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();


            var connectionDB = _configuration["DefaultConnectionDB"];


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

            services.AddApplicationInsightsTelemetry();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", c => c.RequireRole("Admin"));
            });

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

        private static void EnsureTestUsers(DbContextOptions<ApplicationDbContext> identityDbContextOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            using (var db = new ApplicationDbContext(identityDbContextOptions))
            {
                db.Database.EnsureCreated();
            }
        }
    }
}

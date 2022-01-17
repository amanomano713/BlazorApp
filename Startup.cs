using BlazorApp.Data.EF;
using BlazorApp.DataAcess;
using BlazorApp.DataAcess.EF.Extensions;
using BlazorApp.Features.Identity;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            //Connection IdentityUser
            services
                .AddMemoryCache()
                .AddDbContextPool<ApplicationDbContext>(optionsBuilder =>
                {
                    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                    optionsBuilder.EnableSensitiveDataLogging();
                });

            //Connection DB
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), x =>
                {
                    x.MigrationsHistoryTable("MigrationsHistory", "EF");
                    x.CommandTimeout(30);
                });
                options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", c => c.RequireRole("Admin"));
            });

            services.AddRazorPages();
            services.AddControllers();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Pages");
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped<IAccountService, AccountService>();

            //Infraestructure services
            services.AddInfraestrutureServices(Configuration);

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

using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.DataAcess.Infraestructure.Repositories;

namespace BlazorApp.DataAcess.EF.Extensions
{
    public static class IoCExtension
    {
        public static void AddInfraestrutureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserDataRepository, UserDataRepository>(); ;


        }
    }
}

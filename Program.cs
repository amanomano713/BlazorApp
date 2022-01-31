
//Desarrollador: Ing.Armando Rodrigues
namespace BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("From Program, running the host now.");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var root = config.Build();
                config.AddAzureKeyVault($"https://{root["KeyVault:Vault"]}.vault.azure.net/", 
                    root["KeyVault:ClientId"],
                    root["KeyVault:ClientSecret"]);
            })
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>();
             });
    }
}




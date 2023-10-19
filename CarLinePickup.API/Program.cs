using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace CarLinePickup.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((ctx, builder) =>
            {
                builder.AddEnvironmentVariables();

                builder.Build();

                //if (string.IsNullOrEmpty(builtConfig["KeyVaultUrl"])) return;

                //var azureServiceTokenProvider = new AzureServiceTokenProvider();
                //var keyVaultClient = new KeyVaultClient(
                //    new KeyVaultClient.AuthenticationCallback(
                //        azureServiceTokenProvider.KeyVaultTokenCallback));
                //builder.AddAzureKeyVault(
                //    builtConfig["KeyVaultUrl"], keyVaultClient, new DefaultKeyVaultSecretManager());

                //builder.AddAzureAppConfiguration(options =>
                //{
                //    options.Connect(builtConfig["ConnectionStrings:AppConfig"])
                //    .UseFeatureFlags();
                //});
            })
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
    }
}

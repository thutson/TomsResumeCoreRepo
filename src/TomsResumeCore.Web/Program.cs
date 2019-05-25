using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using System;
using System.IO;

namespace TomsResumeCore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("azurekeyvault.json", false, true)
                        .AddEnvironmentVariables();

                    var config = builder.Build();

                    builder.AddAzureKeyVault(
                        $"https://{config["azureKeyVault:vault"]}.vault.azure.net/",
                        config["azureKeyVault:clientId"],
                        config["azureKeyVault:clientSecret"]
                    );
                })
                .UseStartup<Startup>();
    }
}

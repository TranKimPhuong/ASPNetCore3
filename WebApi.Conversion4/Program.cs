using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Serilog;
using System;
using System.IO;

namespace WebApi.Conversion4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();         
            try
            {
                Log.Information("KP__APPLICATION STARTING UP");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "KP__THE APPLICATION FAILED TO START!");
            }
            finally
            {
                Log.Information("KP__TEST FINNALY");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigConfiguration)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        static void ConfigConfiguration(HostBuilderContext hostBuilderContext, IConfigurationBuilder configurationBuilder)
        {
            ////if (hostBuilderContext.HostingEnvironment.IsProduction())
            ////{
            var builtConfig = configurationBuilder
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", false, true)
                                    .AddEnvironmentVariables()
                                    .Build();

            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            // way 1 , follow this https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-3.0#secret-storage-in-the-production-environment-with-azure-key-vault
            //    var keyVaultClient = new KeyVaultClient(
            //        new KeyVaultClient.AuthenticationCallback(
            //            azureServiceTokenProvider.KeyVaultTokenCallback));


            //way 2 , follow to https://c-sharx.net/read-secrets-from-azure-key-vault-in-a-net-core-console-app
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
                        async (string authority, string resource, string scope) => {
                            var authContext = new AuthenticationContext(authority);
                            var credential = new ClientCredential(builtConfig["azureKeyVault:ClientId"],
                                                                  builtConfig["azureKeyVault:ClientSecret"]);
                            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, credential);
                            if (result == null)
                            {
                                throw new InvalidOperationException("Failed to retrieve JWT token");
                            }
                            return result.AccessToken;
                        }
                      ));
            // Calling GetSecretAsync will trigger the authentication code above and eventually
            // retrieve the secret which we can then read.
            var secretBundle = keyVaultClient.GetSecretAsync($"https://{builtConfig["azureKeyVault:KeyVaultName"]}.vault.azure.net/", "secretkey");

            configurationBuilder.AddAzureKeyVault(
                                $"https://{builtConfig["azureKeyVault:KeyVaultName"]}.vault.azure.net/",
                                keyVaultClient,
                                new DefaultKeyVaultSecretManager());
            //}
        }
    }
}

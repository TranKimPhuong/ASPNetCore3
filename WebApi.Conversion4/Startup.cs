using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.WindowsAzure.Storage;
using Serilog;
using Serilog.Events;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace WebApi.Conversion4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Initalize Log
            var logConnectionString = _configuration["LogStorageConnectionString"];
            logConnectionString = "localhost";
            var now = DateTime.UtcNow;

            var logger = new LoggerConfiguration();
            if ("localhost".Equals(logConnectionString))
            {
                string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                var path = Path.Combine(projectDir, "Logs", string.Concat(logConnectionString, now.ToString("'_'yyyyMMdd'.log'")));
                logger.WriteTo.File(path, outputTemplate: "[{Timestamp:G} {Level:u3] {Message}{NewLine:1}{Exception:1}");
            }
            else
            {// chưa test
                var prefixTable = AppDomain.CurrentDomain.FriendlyName;
                if (!string.IsNullOrEmpty(prefixTable))
                {
                    if (prefixTable.StartsWith("WebApi.", StringComparison.OrdinalIgnoreCase))
                        prefixTable = prefixTable.Replace("WebApi.", "Local");
                    else
                        prefixTable = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(prefixTable);
                }

                var tblName = new StringBuilder(prefixTable);
                tblName.Append(now.ToString("'Log'yyyyMMdd"));

                var storageAccount = CloudStorageAccount.Parse(logConnectionString);
                  logger.WriteTo.AzureTableStorage(storageAccount, LogEventLevel.Information, storageTableName: tblName.ToString());
            }

            Log.Logger = logger.CreateLogger();

            #endregion

            #region ko bit don code cu nay xai khi nao, co lien quan toi bear authentication
            //config.SuppressHostPrincipal();
            //config.Filters.Add(new WebApiFilter(ConfigHelper.WEB_API_KEY));
            //config.Filters.Add(new WebApiExceptionFilter());
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}

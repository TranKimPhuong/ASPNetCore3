using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Text;
using WebApi.Conversion4.Ultilities.Config;
using WebApi.Conversion4.Ultilities.KeyVault;
using WebApi.ConversionNew.Services.CustomerA.MasterData;
using WebApi.ConversionNew.Services.CustomerA.Payment;

namespace WebApi.Conversion4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            // follow to https://stackoverflow.com/questions/39231951/how-do-i-access-configuration-in-any-class-in-asp-net-core
            // advance way
            ConversionConfig.Configuration = configuration;
        }

        public IConfiguration _configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Initalize Log
            var logConnectionString = AzureKeyVaultProvider.LogStorageConnectionString;
            logConnectionString = "localhost";
            var now = DateTime.UtcNow;

            var logger = new LoggerConfiguration();
            if ("localhost".Equals(logConnectionString))
            {
                string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                var path = Path.Combine(projectDir, "Logs", string.Concat(logConnectionString, now.ToString("'_'yyyyMMdd'.log'")));
                logger.WriteTo.File(path, outputTemplate: "[{Timestamp:G} {Level:u3}] [{SourceContext}] {Message}{NewLine:1}{Exception:1}");
            }
            else
            {
                // serilog or Nlog 
                // must enable 'Diagnostic logs' to write into Log Stream 
                logger.WriteTo.AzureApp(Serilog.Events.LogEventLevel.Verbose, outputTemplate : "{ Message}{ NewLine}{ Exception}");
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

            Debug();
        }

        public void Debug()
        {
            var inputpath = @"C:\KP\MySampleProjects\ASPNetCoreAPI\Input files\CityofMountJuliet_Vendors.xlsx";
            var inputfile = File.ReadAllBytes(inputpath);
            var conversion = new CustomerASupplierPsTool();
            // register to read content of excel file
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var result = conversion.ProcessDataFile(inputfile);
            File.WriteAllText(@"C:\KP\MySampleProjects\ASPNetCoreAPI\Input files\Master.txt", result.ToString());

            //var inputpath = @"E:\SaaS_ko up\Input for debug code\city of mount juliet\Check Run 5-21-19";
            //var inputfile = File.ReadAllBytes(inputpath);
            //var conversion = new CustomerAPaymentPsTool();
            //// register to read content of excel file
            ////Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //var result = conversion.ProcessDataFile(inputfile);
            //File.WriteAllText(@"E:\SaaS_ko up\Input for debug code\city of mount juliet\Payment.txt", result.ToString());

        }
    }
}

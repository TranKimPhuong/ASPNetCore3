using Serilog;

namespace WebApi.SerilogDemo.Test
{
    public class Apps
    {
        // standard way
        //private readonly static ILogger log = Log.ForContext(typeof(Apps));

        // advance way, overwrite SourceContext
        private readonly static ILogger log = Log.ForContext("SourceContext", "TestLogonAnotherClass");

       
        public static void Run()
        {
            // way 1: chon cách nay2 đi cho nó dễ hiểu
            //var ttt = TestConfigManager.Configuration["Test:name"];
            var ttt1 = TestConfigManager.GetValue("Test:name");

            //way 2
            var appSettingsJson = AppSettingsJson.GetAppSettings();
            string ee = appSettingsJson["Test:name"];

            log.Information("Testing 123");
            log.Information("Testing 456");
            log.Information("Testing 789");
        }
    }
}


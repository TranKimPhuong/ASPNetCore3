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
            log.Information("Testing 123");
            log.Information("Testing 456");
            log.Information("Testing 789");
        }
    }
}


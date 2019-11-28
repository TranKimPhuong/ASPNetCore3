using Serilog;

namespace WebApi.SerilogDemo.Test
{
    public class Apps
    {
        private readonly static ILogger log = Log.ForContext(typeof(Apps));

        public static void Run()
        {
            log.Information("Testing 123");
            log.Information("Testing 456");
            log.Information("Testing 789");
        }
    }
}


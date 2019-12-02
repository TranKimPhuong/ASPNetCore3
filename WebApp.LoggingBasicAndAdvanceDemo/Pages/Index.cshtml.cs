using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.LoggingBasicAndAdvanceDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;

        public IndexModel(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("DemoCategoryName");
        }

        //// The standard way of capturing the category
        //private readonly ILogger<IndexModel> _logger;

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public void OnGet()
        {
            _logger.LogInformation("SHOW LOG FROM PAGES/CONTROLLERS BASE ON DEFAULT ILogger");
            _logger.LogInformation(LoggingId.DemoCode, "CREATE  A LOGGINGID CLASS TO DEFINE EvenID");

            //// The logging Levels
            try
            {
                _logger.LogTrace("TRACE LOG");
                _logger.LogWarning("WARNING LOG");
                _logger.LogDebug("DEBUG LOG");

                throw new Exception("TEST CRITICAL AND ERROR LOG");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex,"CRITICAL LOG: The server wnet down temporary at {Time}", DateTime.UtcNow);  //app is crashed, losing data, server get stuck ...
                _logger.LogError("ERROR LOG AT {Time}", DateTime.UtcNow);
            }
        }
    }
    public class LoggingId
    {
        public const int DemoCode = 1001;

    }
}

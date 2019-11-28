using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.SerilogDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("YOU REQUESTED THE INDEX PAGE");
            try
            {
                for (int i= 0; i < 20;i++)
                {
                    if (i == 10) throw new Exception("THIS IS OUT DEMO EXCEPTION");
                    else
                        _logger.LogInformation("THE VALUE OF I = {LoopcountValue}", i);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "WE CAUGHT THIS EXCEPTION IN THE INDEX GET CALL.");
            }
        }
    }
}

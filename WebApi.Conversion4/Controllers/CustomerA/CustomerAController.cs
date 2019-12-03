using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Conversion4.Controllers.CustomerA
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAController : ControllerBase
    {
        private readonly static ILogger log = Log.ForContext(typeof(CustomerAController));

        [Route("MasterDataConversion/Import")]
        [HttpGet]
        public ActionResult Get()
        {
            log.Information("Testing 123");
            log.Information("Testing 456");
            log.Information("Testing 789");
            return Ok("hoho");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi.Common.Models;
using WebApi.Conversion4.Services.MasterData;

namespace WebApi.Conversion4.Controllers
{
    [ApiController]
    [Route("api/MasterDataConversion/Import")]
    public class MasterDataConversionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MasterDataConversionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //POST: api/MasterDataConversion
        [HttpPost]
        public ActionResult<MessageResponse> Import([FromForm] MasterConversionRequest request)
        {
            var service = new MasterDataConversionService(new MasterDataPsTool(), _configuration);
            return service.ProcessHttpRequest(request);
            
        }
    }
}
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
        //POST: api/MasterDataConversion/Import
        [HttpPost]
        public ActionResult<MessageResponse> Import([FromForm] MasterConversionRequest request)
        {
            var service = new MasterDataConversionService(new MasterDataPsTool());
            return service.ProcessHttpRequest(request);
            
        }
    }
}
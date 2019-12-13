using Microsoft.AspNetCore.Mvc;
using WebApi.Conversion4.Models;
using WebApi.Conversion4.Services.Payment;

namespace WebApi.Conversion4.Controllers
{
    [ApiController]
    [Route("api/PaymentConversion/Import")]
    public class PaymentConversionController : ControllerBase
    {
        // POST: api/PaymentConversion/Import
        [HttpPost]
        public MessageResponse PaymentImport([FromForm] ConversionRequest request)
        {
            PaymentConversionService service = new PaymentConversionService(new PaymentPsTool());
            return service.ProcessRequest(request);
        }
    }
}
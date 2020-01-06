using Microsoft.AspNetCore.Mvc;
using WebApi.Conversion4.Models;
using WebApi.Conversion4.Services;
using WebApi.ConversionNew.Services.CustomerA.Payment;

namespace WebApi.Conversion4.Controllers
{
    [ApiController]
    //[Route("api/{SiteName}/[controller]")]
    public class PaymentsController : ControllerBase
    {
        // OR POST: api/CutomerA/Payments
        [Route("api/CutomerA/Payments")]
        [HttpPost]
        public MessageResponse PaymentImport([FromForm] ConversionRequest request)
        {
            PaymentConversionService service = new PaymentConversionService(new CustomerAPaymentPsTool());
            return service.ProcessRequest(request);
        }
    }
}
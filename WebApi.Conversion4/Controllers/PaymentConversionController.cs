using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Common.Models;
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
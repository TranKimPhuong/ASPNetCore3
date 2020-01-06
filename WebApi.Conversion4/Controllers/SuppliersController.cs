using Microsoft.AspNetCore.Mvc;
using WebApi.Conversion4.Models;
using WebApi.Conversion4.Services;
using WebApi.ConversionNew.Services.CustomerA.MasterData;

namespace WebApi.Conversion4.Controllers
{
    [ApiController]
    [Route("api/{SiteName}/[controller]")]
    public class SuppliersController : ControllerBase
    {
        // OR POST: api/CutomerA/Suppliers
        [Route("api/CutomerA/Suppliers")]
        [HttpPost]
        public ActionResult<MessageResponse> Import([FromForm] MasterConversionRequest request)
        {
            var service = new SupplierConversionService(new CustomerASupplierPsTool());
            return service.ProcessHttpRequest(request);
            
        }
    }
}
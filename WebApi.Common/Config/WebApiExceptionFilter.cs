using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;
using System.Net.Http;
using System.Reflection;
using WebApi.Common.Helper;

namespace WebApi.Common.Config
{
    public class WebApiExceptionFilter : ActionFilterAttribute
    {
        private static ILog LOGGER = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //log exception
            LOGGER.Error(context.Exception);

            // follow to https://stackoverflow.com/questions/47898410/access-controller-from-exceptionfilterattribute-in-asp-net-core
            // and CustomHandleErrorFilter.cs
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = new ViewResult { ViewName = "Error" };
            var modelMetadata = new EmptyModelMetadataProvider();
            result.ViewData = new ViewDataDictionary(modelMetadata, context.ModelState);
            result.ViewData.Add("HandleException", MessageHelper.ERR_APP);

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
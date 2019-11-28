using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;
using System.Reflection;
using WebApi.Common.Helper;

namespace WebApi.Common.Config
{
    public class CustomHandleErrorFilter : ExceptionFilterAttribute
    {
        private static ILog LOGGER = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnException(ExceptionContext filterContext)
        {
            LOGGER.Error(filterContext.Exception);
            // Follow to https://stackoverflow.com/questions/55784717/net-core-2-equivalent-of-handleerrorattribute

            if (filterContext.Exception != null)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    
                var result = new ViewResult { ViewName = "Error" };
                var modelMetadata = new EmptyModelMetadataProvider();
                result.ViewData = new ViewDataDictionary(modelMetadata, filterContext.ModelState);
                result.ViewData.Add("HandleException", MessageHelper.ERR_APP);

                filterContext.Result = result;
                filterContext.ExceptionHandled = true;

                // ko bít làm
                //HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
                //cache.SetCacheability(HttpCacheability.NoCache);
            }
            else
            {
                base.OnException(filterContext);
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc.Filters;


namespace WebApi.Common.Config
{
    public class NoCacheGlobalActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // KO BÍT LÀM
            //HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            //cache.SetCacheability(HttpCacheability.NoCache);

            base.OnResultExecuted(filterContext);
        }
    }
}
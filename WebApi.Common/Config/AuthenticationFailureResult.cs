using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace WebApi.Common.Config
{
    public class AuthenticationFailureResult : IActionResult
    {
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        //public Task<HttpResponseMessage> ExecuteResultAsync(CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(ExecuteAsync());
        //}
        // Follow to https://stackoverflow.com/questions/42594356/how-to-write-custom-actionresult-in-asp-net-core
        public Task ExecuteResultAsync(ActionContext context)
        {
            return Task.FromResult(ExecuteAsync());
        }

        private HttpResponseMessage ExecuteAsync()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;
            return response;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Filters;

namespace WebApi.Common.Config
{
    public class WebApiFilter : Attribute, IAuthenticationFilter
    {
        static string AUTH_SCHEME = "Bearer";
        private string secretKey;

        public WebApiFilter(string secretKey)
        {
            this.secretKey = secretKey;
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // 1. Look for credentials in the request.
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            // 2. If there are no credentials, do nothing.
            if (authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing authorization", request);
                return;
            }

            // 3. If there are credentials but the filter does not recognize the 
            //    authentication scheme, do nothing.
            if (!AUTH_SCHEME.Equals(authorization.Scheme))
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid schema", request);
                return;
            }

            // 4. If there are credentials that the filter understands, try to validate them.
            // 5. If the credentials are bad, set the error result.
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }

            string reqToken = authorization.Parameter;
            if (!reqToken.Equals(this.secretKey))
            {
                context.ErrorResult = new StatusCodeResult(HttpStatusCode.Unauthorized, request);
                return;
            }

            //build principal
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "apiKey")
            };
            var id = new ClaimsIdentity(claims, AUTH_SCHEME);
            var principal = new ClaimsPrincipal(new[] { id });
            context.Principal = principal;

            await Task.Yield();
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue(AUTH_SCHEME);
            context.Result = new ResultWithChallenge(context.Result, AUTH_SCHEME);
            return Task.FromResult(0);
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }

    internal class ResultWithChallenge : IHttpActionResult
    {
        private readonly IHttpActionResult next;
        private readonly string authSchema;
        public ResultWithChallenge(IHttpActionResult next, string authSchema)
        {
            this.next = next;
            this.authSchema = authSchema;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var res = await next.ExecuteAsync(cancellationToken);
            if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                res.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(this.authSchema));
            }
            return res;
        }
    }
    
}

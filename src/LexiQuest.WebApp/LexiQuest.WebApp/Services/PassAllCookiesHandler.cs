using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LexiQuest.WebApp.Services;

internal class PassAllCookiesHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor?.HttpContext;
        if (httpContext != null)
        {
            request.Headers.Add("Cookie", string.Join("; ", httpContext.Request.Cookies.Select(c => $"{c.Key}={c.Value}")));
            // var authenticationCookie = httpContext.Request.Cookies[".AspNetCore.Identity.Application"];
            // if (!string.IsNullOrEmpty(authenticationCookie))
            // {
            //     request.Headers.Add("Cookie", new CookieHeaderValue(".AspNetCore.Identity.Application", authenticationCookie).ToString());
            // }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
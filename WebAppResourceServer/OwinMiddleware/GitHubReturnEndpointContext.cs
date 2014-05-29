using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace WebAppResourceServer.OwinMiddleware
{
    public class GitHubReturnEndpointContext : ReturnEndpointContext
    {
        public GitHubReturnEndpointContext(
            IOwinContext context,
            AuthenticationTicket ticket)
            : base(context, ticket)
        {
        }
    }
}
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace WebAppGitHubCodeFlowDemo
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
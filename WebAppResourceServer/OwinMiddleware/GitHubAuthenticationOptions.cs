using Microsoft.Owin.Security;

namespace WebAppResourceServer.OwinMiddleware
{
    public class GitHubAuthenticationOptions : AuthenticationOptions
    {
        public GitHubAuthenticationProvider Provider { get; set; }

        public GitHubAuthenticationOptions()  : base("GitHub")
        {
        }
    }
}
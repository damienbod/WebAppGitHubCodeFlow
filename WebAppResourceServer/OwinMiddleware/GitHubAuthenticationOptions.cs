using Microsoft.Owin.Security;

namespace WebAppResourceServer.OwinMiddleware
{
    public class GitHubAuthenticationOptions : AuthenticationOptions
    {
        public AuthenticationProviderForGitHubOAuth2 Provider { get; set; }

        public GitHubAuthenticationOptions()  : base("GitHub")
        {
        }
    }
}
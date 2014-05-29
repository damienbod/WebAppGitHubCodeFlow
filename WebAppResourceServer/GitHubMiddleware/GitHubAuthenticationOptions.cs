using Microsoft.Owin.Security;

namespace WebAppGitHubCodeFlowDemo
{
    public class GitHubAuthenticationOptions : AuthenticationOptions
    {
        public GitHubAuthenticationProvider Provider { get; set; }

        public GitHubAuthenticationOptions()
            : base("GitHub")
        {
        }
    }
}
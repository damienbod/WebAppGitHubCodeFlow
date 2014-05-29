using System;
using System.Threading.Tasks;

namespace WebAppGitHubCodeFlowDemo
{
    public class GitHubAuthenticationProvider
    {
        public GitHubAuthenticationProvider()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
            OnReturnEndpoint = context => Task.FromResult<object>(null);
        }

        public Func<GitHubAuthenticatedContext, Task> OnAuthenticated { get; set; }
        public Func<GitHubReturnEndpointContext, Task> OnReturnEndpoint { get; set; }
        
        public virtual Task Authenticated(GitHubAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }

        public virtual Task ReturnEndpoint(GitHubReturnEndpointContext context)
        {
            return OnReturnEndpoint(context);
        }
    }
}
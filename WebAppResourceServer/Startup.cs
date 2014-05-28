using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(WebAppGitHubCodeFlowDemo.Startup))]

namespace WebAppGitHubCodeFlowDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ClaimsAuthorization.CustomAuthorizationManager = new AuthorizationManager();
            app.Use(typeof(GitHubAuthenticationMiddleware), app, new GitHubAuthenticationOptions());
            app.UseWebApi(WebApiConfig.Register());
        }
    }

    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            // This is where you can authorise the user if you
            // have special roles etc, database checks or whatever
            return true;
        }
    }

    public class GitHubAuthenticationMiddleware : AuthenticationMiddleware<GitHubAuthenticationOptions>
    {
        public GitHubAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app,
            GitHubAuthenticationOptions options)
            : base(next, options)
        {
        }

        protected override AuthenticationHandler<GitHubAuthenticationOptions> CreateHandler()
        {
            return new GitHubAuthenticationHandler();
        }
    }

    public class GitHubAuthenticationOptions : AuthenticationOptions
    {
        public GitHubAuthenticationOptions()
            : base("GitHub")
        {
        }
    }

    public class GitHubAuthenticationHandler : AuthenticationHandler<GitHubAuthenticationOptions>
    {
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationProperties properties = new AuthenticationProperties();

            // Do my authorization here 
            // Get the Bearer token from the Request.Headers
            return null;

        }
    }
}

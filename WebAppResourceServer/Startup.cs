using System.Globalization;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
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
}

using Microsoft.Owin;
using Owin;
using WebAppResourceServer;
using WebAppResourceServer.OwinMiddleware;

[assembly: OwinStartup(typeof(Startup))]

namespace WebAppResourceServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(AuthenticationMiddlewareForGitHubOAuth2), app, new GitHubAuthenticationOptions());
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}

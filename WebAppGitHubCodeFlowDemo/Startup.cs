using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.IdentityModel.Tokens;
using Thinktecture.IdentityModel;
using Thinktecture.IdentityModel.Tokens;

[assembly: OwinStartup(typeof(WebAppGitHubCodeFlowDemo.Startup))]

namespace WebAppGitHubCodeFlowDemo
{
    public class Startup
    {
        private const string ClientSecret = "03359f5248df067a6dbc504a3f33c7f8bab10df2";

        public void Configuration(IAppBuilder app)
        {
            // authorization manager
            Thinktecture.IdentityModel.ClaimsAuthorization.CustomAuthorizationManager = new AuthorizationManager();

            // no mapping of incoming claims to Microsoft types
            JwtSecurityTokenHandler.InboundClaimTypeMap = ClaimMappings.None;
            // validate JWT tokens from AuthorizationServer
            app.UseJsonWebToken(
                issuer: "github",
                audience: "users",
                signingKey: ClientSecret);

            // claims transformation
            app.UseClaimsTransformation(new ClaimsTransformer().Transform);


            app.UseWebApi(WebApiConfig.Register());
        }
    }

    public class ClaimsTransformer
    {
        public Task<ClaimsPrincipal> Transform(ClaimsPrincipal incomingPrincipal)
        {
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return Task.FromResult(incomingPrincipal);
            }

            return Task.FromResult(incomingPrincipal);
        }
    }

    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            // inspect sub, action, resource
            Debug.WriteLine(context.Principal.FindFirst("sub").Value);
            Debug.WriteLine(context.Action.First().Value);
            Debug.WriteLine(context.Resource.First().Value);

            return true;
        }
    }
}

using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;

namespace WebAppResourceServer.OwinMiddleware
{
    public class GitHubAuthenticatedContext : BaseContext
    {
        public GitHubAuthenticatedContext(IOwinContext context, JObject user, string accessToken)
            : base(context)
        {
            User = user;
            AccessToken = accessToken;

            Id = TryGetValue(user, "id");
            Name = TryGetValue(user, "name");
            Link = TryGetValue(user, "url");
            UserName = TryGetValue(user, "login");
        }

        public JObject User { get; private set; }
        public string AccessToken { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Link { get; private set; }
        public string UserName { get; private set; }
        public ClaimsIdentity Identity { get; set; }
        public AuthenticationProperties Properties { get; set; }

        private static string TryGetValue(JObject user, string propertyName)
        {
            JToken value;
            return user.TryGetValue(propertyName, out value) ? value.ToString() : null;
        }
    }
}
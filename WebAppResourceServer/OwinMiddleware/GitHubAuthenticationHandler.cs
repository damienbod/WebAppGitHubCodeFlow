using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json.Linq;

namespace WebAppResourceServer.OwinMiddleware
{
    public class GitHubAuthenticationHandler : AuthenticationHandler<GitHubAuthenticationOptions>
    {
        private const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";

        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public GitHubAuthenticationHandler(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            try
            {
                // Get the token from the header
                var tokenHeader = Request.Headers.Get("Authorization");
                var token = tokenHeader.Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                // Get the GitHub user
                var userRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/user?access_token=" + Uri.EscapeDataString(token));
                userRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage userResponse = await _httpClient.SendAsync(userRequest, Request.CallCancelled);
                userResponse.EnsureSuccessStatusCode();
                var text = await userResponse.Content.ReadAsStringAsync();
                JObject user = JObject.Parse(text);

                var context = new GitHubAuthenticatedContext(Context, user, token)
                {
                    Identity = new ClaimsIdentity(
                        Options.AuthenticationType,
                        ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType)
                };

                if (!string.IsNullOrEmpty(context.Id))
                {
                    context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.Id, XmlSchemaString, Options.AuthenticationType));
                }
                if (!string.IsNullOrEmpty(context.UserName))
                {
                    context.Identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, context.UserName, XmlSchemaString, Options.AuthenticationType));
                }
                if (!string.IsNullOrEmpty(context.Name))
                {
                    context.Identity.AddClaim(new Claim("urn:github:name", context.Name, XmlSchemaString, Options.AuthenticationType));
                }
                if (!string.IsNullOrEmpty(context.Link))
                {
                    context.Identity.AddClaim(new Claim("urn:github:url", context.Link, XmlSchemaString, Options.AuthenticationType));
                }

                await Options.Provider.Authenticated(context);

                return new AuthenticationTicket(context.Identity, context.Properties);
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex.Message);
            }
            return new AuthenticationTicket(null, null);
        }

    }
}
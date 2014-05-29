using System.Net.Http;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace WebAppResourceServer.OwinMiddleware
{
    public class AuthenticationMiddlewareForGitHubOAuth2 : AuthenticationMiddleware<GitHubAuthenticationOptions>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public AuthenticationMiddlewareForGitHubOAuth2(Microsoft.Owin.OwinMiddleware next, IAppBuilder app, GitHubAuthenticationOptions options) : base(next, options)
        {
            if (Options.Provider == null)
            {
                Options.Provider = new AuthenticationProviderForGitHubOAuth2();
            }

            _logger = app.CreateLogger<AuthenticationMiddlewareForGitHubOAuth2>();

            _httpClient = new HttpClient(new WebRequestHandler());
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Owin GitHub middleware to validate token");
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
        }

        protected override AuthenticationHandler<GitHubAuthenticationOptions> CreateHandler()
        {
            return  new AuthenticationHandlerForGitHubTokenValidation(_httpClient, _logger);
        }
    }
}
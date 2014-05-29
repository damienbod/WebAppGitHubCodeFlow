using System.Net.Http;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace WebAppResourceServer.OwinMiddleware
{
    public class GitHubAuthenticationMiddleware : AuthenticationMiddleware<GitHubAuthenticationOptions>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public GitHubAuthenticationMiddleware(Microsoft.Owin.OwinMiddleware next, IAppBuilder app, GitHubAuthenticationOptions options) : base(next, options)
        {
            if (Options.Provider == null)
            {
                Options.Provider = new GitHubAuthenticationProvider();
            }

            _logger = app.CreateLogger<GitHubAuthenticationMiddleware>();

            _httpClient = new HttpClient(new WebRequestHandler());
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Owin GitHub middleware to validate token");
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
        }

        protected override AuthenticationHandler<GitHubAuthenticationOptions> CreateHandler()
        {
            return  new GitHubAuthenticationHandler(_httpClient, _logger);
        }
    }
}
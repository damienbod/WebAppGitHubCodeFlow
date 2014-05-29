using System;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace WebAppGitHubCodeFlowDemo
{
    public class GitHubAuthenticationMiddleware : AuthenticationMiddleware<GitHubAuthenticationOptions>
    {
        private readonly HttpClient httpClient;
        private readonly ILogger logger;

        public GitHubAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app,
            GitHubAuthenticationOptions options)
            : base(next, options)
        {

            if (Options.Provider == null)
                Options.Provider = new GitHubAuthenticationProvider();

            logger = app.CreateLogger<GitHubAuthenticationMiddleware>();

            httpClient = new HttpClient(new WebRequestHandler())
            {
                MaxResponseContentBufferSize = 1024 * 1024 * 10,
            };
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft Owin GitHub middleware");
            httpClient.DefaultRequestHeaders.ExpectContinue = false;

        }

        protected override AuthenticationHandler<GitHubAuthenticationOptions> CreateHandler()
        {
            return  new GitHubAuthenticationHandler(httpClient, logger);
        }
    }
}
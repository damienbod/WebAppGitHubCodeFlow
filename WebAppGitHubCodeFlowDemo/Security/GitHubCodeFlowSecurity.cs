using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Thinktecture.IdentityModel.Client;

namespace WebAppGitHubCodeFlowDemo.Security
{
    public class GitHubCodeFlowSecurity
    {
        private const string OAuth2AuthorizeEndpoint = "https://github.com/login/oauth/authorize";
        private const string OAuth2TokenEndpoint = "https://github.com/login/oauth/access_token";
        private const string ClientId = "192b91f57fde31898794";
        private const string ClientSecret = "03359f5248df067a6dbc504a3f33c7f8bab10df2";
        private const string RedirectUrl = "http://localhost:60703/DemoOath2CodeFlowMvc/callback";
        private const string Scopes = "user";
        private string State = "29385860478549569433784333";

        public string CreateCodeFlowUrl()
        {
            var client = new OAuth2Client(new Uri(OAuth2AuthorizeEndpoint));
            return client.CreateCodeFlowUrl(ClientId, Scopes, RedirectUrl, State);           
        }
        public async Task<TokenData> GetToken(string code)
        {
            var client = new HttpClient();
            string parameters = "client_id=" + HttpUtility.UrlEncode(ClientId)
                + "&redirect_uri=" + HttpUtility.UrlEncode(RedirectUrl) + "&client_secret=" + HttpUtility.UrlEncode(ClientSecret)
                + "&code=" + HttpUtility.UrlEncode(code) + "&state=" + State;

            var uri = new Uri(OAuth2TokenEndpoint + "?" + parameters);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsync(uri, new StringContent(parameters));
            var tokenData = await response.Content.ReadAsAsync<TokenData>();
            return tokenData;
        }
    }
}
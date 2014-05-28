using System.Web.Http;

namespace WebAppGitHubCodeFlowDemo
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            return config;
        }
    }
}

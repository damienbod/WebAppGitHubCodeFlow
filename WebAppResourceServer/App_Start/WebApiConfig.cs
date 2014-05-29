using System.Web.Http;

namespace WebAppResourceServer
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

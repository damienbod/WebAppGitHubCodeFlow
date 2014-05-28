using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAppGitHubCodeFlow;

namespace WebAppGitHubCodeFlowDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAppGitHubCodeFlowDemo.Security;

namespace WebAppGitHubCodeFlowDemo.Controllers
{
    public class DemoOath2CodeFlowMvcController : Controller
    {
        private GitHubCodeFlowSecurity _gitHubCodeFlowSecurity;
        public DemoOath2CodeFlowMvcController()
        {
            _gitHubCodeFlowSecurity = new GitHubCodeFlowSecurity();
        }

        public ActionResult Index()
        {
            ViewBag.AuthorizeUrl = _gitHubCodeFlowSecurity.CreateCodeFlowUrl();
            ViewBag.Title = "Demo Oath2 Code Flow";
            return View();
        }

        public ActionResult Callback()
        {
            ViewBag.Code = Request.QueryString["code"] ?? "none";
            ViewBag.Error = Request.QueryString["error"] ?? "none";
            return View();
        }

        public async Task<ActionResult> SecureData(string code)
        {
            TokenData content = await _gitHubCodeFlowSecurity.GetToken(code);
            string data = await GetResourceSecureData(content.Access_Token, 4);
            ViewBag.SecureData = data;
            return View();
        }

        private async Task<string> GetResourceSecureData(string token, int id)
        {
            string secureData = "";
            var client = new HttpClient();
            client.SetBearerToken(token);

            client.BaseAddress = new Uri("http://localhost:50182/api/ResourceServerWebApi/" + id);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                secureData = await response.Content.ReadAsAsync<string>();
            }
            return secureData;
        }
        
    }
}

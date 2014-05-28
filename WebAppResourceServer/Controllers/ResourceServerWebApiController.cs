using System.Collections.Generic;
using System.Web.Http;

namespace WebAppGitHubCodeFlowDemo.Controllers
{
    [RoutePrefix("api/ResourceServerWebApi")]
    public class ResourceServerWebApiController : ApiController
    {
        [Authorize] // http://localhost:50182/
        [HttpGet]
        [Route("{id}")]
        public string Get(int id)
        {
            return "secureValue";
        }

    }
}

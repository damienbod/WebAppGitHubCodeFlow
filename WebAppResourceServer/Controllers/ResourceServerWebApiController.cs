using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace WebAppResourceServer.Controllers
{
    [RoutePrefix("api/ResourceServerWebApi")]
    public class ResourceServerWebApiController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public string Get(int id)
        {
            var sb = new StringBuilder();
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            foreach (var claim in claims)
            {
                sb.AppendLine(claim.Type + ":" + claim.Value);
            }
            return sb.ToString();
        }

    }
}

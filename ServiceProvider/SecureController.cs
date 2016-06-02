using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace ServiceProvider
{
    [Authorize, Route("Secure")]
    public class SecureController : ApiController
    {
        public IHttpActionResult Get()
        {
            var identity = (ClaimsIdentity)RequestContext.Principal.Identity;
            return Ok<IList<Claim>>(identity.Claims.ToList());
        }
    }
}

using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.WsFederation.Models;
using IdentityModel.Constants;

namespace IdentityService.Config
{
    public class RelyingParties
    {
        public static IEnumerable<RelyingParty> Get()
        {
            return new List<RelyingParty>
            {
                new RelyingParty
                {
                    Realm = "https://localhost:44300/",
                    Name = "AdfsClient",
                    Enabled = true,
                    ReplyUrl = "https://localhost:44300/",
                    TokenType = TokenTypes.Saml2TokenProfile11,
                    TokenLifeTime = 1,
                    IncludeAllClaimsForUser = true,
                    //ClaimMappings = new Dictionary<string, string>
                    //{
                    //    { "sub", ClaimTypes.NameIdentifier },
                    //    { "name", ClaimTypes.Name },
                    //    { "given_name", ClaimTypes.GivenName },
                    //    { "email", ClaimTypes.Email }
                    //}
                }
            };
        }
    }
}

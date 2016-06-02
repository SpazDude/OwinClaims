using System.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using Owin;

namespace ServiceProvider
{
    public partial class Startup
    {
        private const string FedAuth = "WS-Fed Auth (Primary)";

        public void ConfigureWsFederationAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseWsFederationAuthentication(new WsFederationAuthenticationOptions
            {
                AuthenticationType = FedAuth,
                Wtrealm = ConfigurationManager.AppSettings["app:URI"],
                MetadataAddress = ConfigurationManager.AppSettings["wsFederation:MetadataEndpoint"],
            });
        }
    }
}
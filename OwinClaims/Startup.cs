using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.WsFederation;
using System.Configuration;
using System.IO;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Threading.Tasks;
using System.Text;
using System.Security.Claims;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;

[assembly: OwinStartup(typeof(WebApplication1.Startup))]

namespace WebApplication1
{
    public class Startup
    {
        private const string AuthenticationType = "WS-Fed Auth (Primary)";

        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseWsFederationAuthentication(new WsFederationAuthenticationOptions
            {
                AuthenticationType = AuthenticationType,
                Wtrealm = ConfigurationManager.AppSettings["app:URI"],
                MetadataAddress = ConfigurationManager.AppSettings["wsFederation:MetadataEndpoint"],
            });

            app.UseErrorPage();

            app.UseFileServer(new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
            });

            app.Use((context, continuation) =>
            {
                if (context.Authentication.User?.Identity != null &&
                    context.Authentication.User.Identity.IsAuthenticated ||
                    context.Request.Path.Value != "/secure")
                {
                    return continuation();
                }
                context.Authentication.Challenge(AuthenticationType);
                return Task.Delay(0);
            });

            app.Use((context, continuation) =>
            {
                if (context.Request.Path.Value == "/secure")
                {
                    var builder = new StringBuilder();
                    var identity = (ClaimsIdentity)context.Request.User.Identity;
                    foreach (var claim in identity.Claims)
                    {
                        builder.AppendFormat("{0} : {1}", claim.Type, claim.Value);
                        builder.AppendLine();
                    }
                    context.Response.ContentType = "text/plain";
                    return context.Response.WriteAsync(builder.ToString());
                }
                return continuation();
            });

            app.UseWelcomePage();
        }
    }
}

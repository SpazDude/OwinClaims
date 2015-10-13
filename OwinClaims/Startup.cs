using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.WsFederation;
using System.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Threading.Tasks;
using System.Text;
using System.Security.Claims;

[assembly: OwinStartup(typeof(WebApplication1.Startup))]

namespace WebApplication1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseWsFederationAuthentication(new WsFederationAuthenticationOptions
            {
                AuthenticationType = "WS-Fed Auth (Primary)",
                Wtrealm = ConfigurationManager.AppSettings["app:URI"],                
                MetadataAddress = ConfigurationManager.AppSettings["wsFederation:MetadataEndpoint"],
            });

            AuthenticateAllRequests(app, "WS-Fed Auth (Primary)");

            app.Run(context =>
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
            });
        }

        private static void AuthenticateAllRequests(IAppBuilder app, params string[] authenticationTypes)
        {
            app.Use((context, continuation) =>
            {
                if (context.Authentication.User != null &&
                    context.Authentication.User.Identity != null &&
                    context.Authentication.User.Identity.IsAuthenticated)
                {
                    return continuation();
                }
                else
                {
                    context.Authentication.Challenge(authenticationTypes);
                    return Task.Delay(0);
                }
            });
        }
    }
}

using System.Configuration;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ServiceProvider.Startup))]

namespace ServiceProvider
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            if (ConfigurationManager.AppSettings.AllKeys.Contains("wsFederation:MetadataEndpoint"))
                ConfigureWsFederationAuth(app);
            else
                ConfigureSaml2Auth(app);

            app.UseErrorPage();

            var httpConfig = new HttpConfiguration();
            httpConfig.MapHttpAttributeRoutes();
            app.UseWebApi(httpConfig);

            app.UseWelcomePage();
        }
    }
}

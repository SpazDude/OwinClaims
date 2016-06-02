using System;
using System.IdentityModel.Metadata;
using Kentor.AuthServices;
using Kentor.AuthServices.Owin;
using Kentor.AuthServices.Configuration;
using Kentor.AuthServices.WebSso;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ServiceProvider
{
    public partial class Startup
    {
        private const string SignInAsType = "kentorStub";

        public void ConfigureSaml2Auth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //LoginPath = new PathString("/Account/Login"),
                //Provider = new CookieAuthenticationProvider
                //{
                //    // Enables the application to validate the security stamp when the user logs in.
                //    // This is a security feature which is used when you change a password or add an external login to your account.  
                //    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                //        validateInterval: TimeSpan.FromMinutes(30),
                //        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                //}
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var authServicesOptions = new KentorAuthServicesAuthenticationOptions(false)
            {
                SPOptions = new SPOptions
                {
                    EntityId = new EntityId("http://sp.example.com"),
                    ReturnUrl = new Uri("http://localhost:1992/Account/ExternalLoginCallback"),
                },

                SignInAsAuthenticationType = SignInAsType,
                AuthenticationType = "saml2p",
                Caption = "SAML2p",
            };

            authServicesOptions.IdentityProviders.Add(new IdentityProvider(
              new EntityId("http://stubidp.kentor.se/Metadata"),
              authServicesOptions.SPOptions)
            {
                AllowUnsolicitedAuthnResponse = true,
                Binding = Saml2BindingType.HttpRedirect,
                LoadMetadata = true,
            });

            app.UseKentorAuthServicesAuthentication(authServicesOptions);
        }
    }
}
using System;
using AxesoWeb.App_Start;
using AxesoWeb.Models;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace AxesoWeb
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Account/Index"),
                SlidingExpiration = true
            });
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "1092140892133-8djgn3b1j3p5teh9gpevjvvovq5q2hob.apps.googleusercontent.com",
                ClientSecret = "SQHIcdPmbvEsEbGDUfZ6p8Z0",
                CallbackPath = new PathString("/GoogleLoginCallback")
            });

            app.UseFacebookAuthentication(
               appId: "2676568482629515",
               appSecret: "9cb7235e406b4dc104db723c2c0b9516");

            
        }
    }
}
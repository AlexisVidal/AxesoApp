using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AxesoConsumer.Droid.Services;
using AxesoConsumer.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(GoogleAuthService))]
namespace AxesoConsumer.Droid.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        internal static MainActivity MainActivity { get; set; }

        public GoogleAuthService()
        {

        }

        public void Autheticate(IGoogleAuthenticationDelegate googleAuthenticationDelegate)
        {
            GoogleAuthenticatorHelper.Auth = new GoogleAuthenticator(
               "1092140892133-bba5rp6hghh1td9fvsrqae8bl2pt3rlb.apps.googleusercontent.com",
               "https://www.googleapis.com/auth/userinfo.email",
               "com.jdc.OAuth:/oauth2redirect",
               googleAuthenticationDelegate);

            // Display the activity handling the authentication
            var authenticator = GoogleAuthenticatorHelper.Auth.GetAuthenticator();
            var intent = authenticator.GetUI(MainActivity);
            MainActivity.StartActivity(intent);
        }
    }
}

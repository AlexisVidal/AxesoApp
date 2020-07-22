using AxesoConsumer.iOS.Services;
using AxesoConsumer.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(GoogleAuthService))]
namespace AxesoConsumer.iOS.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        public GoogleAuthService()
        {

        }

        public void Autheticate(IGoogleAuthenticationDelegate googleAuthenticationDelegate)
        {
            GoogleAuthenticatorHelper.Auth = new GoogleAuthenticator(
               "1092140892133-bba5rp6hghh1td9fvsrqae8bl2pt3rlb.apps.googleusercontent.com",
               "email",
               "com.jdc.OAuth:/oauth2redirect",
               googleAuthenticationDelegate);

            GoogleAuthenticatorHelper.Auth.AuthenticationDone += Auth_AuthenticationDone;

            // Display the activity handling the authentication
            var authenticator = GoogleAuthenticatorHelper.Auth.GetAuthenticator();

            var viewController = authenticator.GetUI();
            var currentViewController = GetCurrentViewController();
            currentViewController.PresentViewController(viewController, true, null);
        }

        private void Auth_AuthenticationDone()
        {
            CloseBrowser();
        }

        public void CloseBrowser()
        {
            var currentViewController = GetCurrentViewController();
            currentViewController.DismissViewController(true, null);
        }

        private UIViewController GetCurrentViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            return vc;
        }
    }
}

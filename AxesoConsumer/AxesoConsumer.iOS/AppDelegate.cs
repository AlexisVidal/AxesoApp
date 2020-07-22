using System;
using System.Collections.Generic;
using System.Linq;
using AxesoConsumer.Helpers;
using AxesoConsumer.Services;
using FFImageLoading.Forms.Platform;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Lottie.Forms.iOS.Renderers;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.XForms.iOS.TextInputLayout;
using Syncfusion.XForms.Pickers.iOS;
using UIKit;

namespace AxesoConsumer.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIwNjc4QDMxMzcyZTM0MmUzMEdLN2NIeUV6dUE4Nnc0UGpCNGdHRndrVFVmeHljazlzdkxYcGVmZTJZUkk9");
            Xamarin.FormsMaps.Init();
            new Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer();
            global::Xamarin.Forms.Forms.Init();
            //global::Xamarin.Forms.FormsMaterial.Init();
            global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();
            LoadApplication(new App());
            FacebookClientManager.OnActivated();
            FacebookClientManager.Initialize(app, options);
            GoogleClientManager.Initialize();
            SfTextInputLayoutRenderer.Init();
            //CachedImageRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            ImageCircleRenderer.Init();
            AnimationViewRenderer.Init();
            //ImageCircleRenderer.Init();
            //CarouselViewRenderer.Init();
            CachedImageRenderer.InitImageSourceHandler();
            SfDatePickerRenderer.Init();
            return base.FinishedLaunching(app, options);
        }
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            // Convert NSUrl to Uri
            var uri = new Uri(url.AbsoluteString);

            

            return true;
        }
    }
}

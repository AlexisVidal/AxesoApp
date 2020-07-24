using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Plugin.FacebookClient;
using Xamarin.Facebook;
using Android;
using Plugin.GoogleClient;
using FFImageLoading.Forms.Platform;
using ImageCircle.Forms.Plugin.Droid;
using Lottie.Forms.Droid;
using Firebase.Database;
using System.Collections.Concurrent;
using Axeso_BE;
using System.Collections.Generic;
using AxesoConsumer.Models;
//using Lottie.Forms;

namespace AxesoConsumer.Droid
{
    [Activity(Label = "Axeso Consumidor", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = false, 
        ConfigurationChanges = ConfigChanges.ScreenSize | 
        ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        FirebaseClient firebase;
        private ConcurrentDictionary<string, Pedido> pedidoList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIwNjc4QDMxMzcyZTM0MmUzMEdLN2NIeUV6dUE4Nnc0UGpCNGdHRndrVFVmeHljazlzdkxYcGVmZTJZUkk9");
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            base.OnCreate(savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            //FacebookSdk.SdkInitialize(this);
            FacebookClientManager.Initialize(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            //GoogleClientManager.Initialize(this,null, "314791326411-c4c1cpl3vuvtrpu167t2eodic0iok82q.apps.googleusercontent.com");
            GoogleClientManager.Initialize(this,null, "940121152350-pta1k5gvh419nmj31djav046n5q7fd0m.apps.googleusercontent.com");
            //CachedImageRenderer.Init(true);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
            AnimationViewRenderer.Init();
            //CarouselViewRenderer.Init();
            CachedImageRenderer.InitImageViewHandler();
            LoadApplication(new App());
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#98CA3F"));
            if ((CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) != (int)Permission.Granted))
            {
                RequestPermissions(new string[] { Manifest.Permission.AccessCoarseLocation }, 0);
            }
            if ((CheckSelfPermission(Manifest.Permission.AccessFineLocation) != (int)Permission.Granted) )
            {
                RequestPermissions(new string[] { Manifest.Permission.AccessFineLocation}, 0);
            }
            if ((CheckSelfPermission(Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted))
            {
                RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            }
            if ((CheckSelfPermission(Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted))
            {
                RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage }, 0);
            }
            firebase = new FirebaseClient("https://https://axesoapp-a802c.firebaseio.com/");
            PopulateList();
        }

        private async void PopulateList()
        {
            var firebase = new FirebaseClient("https://https://axesoapp-a802c.firebaseio.com/");
            firebase.Child("axesoapp-a802c").AsObservable<PedidoLineaTiempos>().Subscribe(obs =>
            {
                switch (obs.EventType)
                {
                    case Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate:
                        break;
                    case Firebase.Database.Streaming.FirebaseEventType.Delete:
                        break;
                    default:
                        break;
                }
            });
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnStart()
        {
            base.OnStart();
            //if ((CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) != (int)Permission.Granted))
            //{
            //    RequestPermissions(new string[] { Manifest.Permission.AccessCoarseLocation }, 0);
            //}
            //if ((CheckSelfPermission(Manifest.Permission.AccessFineLocation) != (int)Permission.Granted))
            //{
            //    RequestPermissions(new string[] { Manifest.Permission.AccessFineLocation }, 0);
            //}
            //if ((CheckSelfPermission(Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted))
            //{
            //    RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            //}
            //if ((CheckSelfPermission(Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted))
            //{
            //    RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage }, 0);
            //}
        }
        protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            GoogleClientManager.OnAuthCompleted(requestCode, resultCode, data);
            FacebookClientManager.OnActivityResult(requestCode, resultCode, data);
        }
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}
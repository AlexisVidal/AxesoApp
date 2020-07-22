using AxesoConsumer.Data;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using AxesoConsumer.Repositories;
using AxesoConsumer.ViewModels;
using AxesoConsumer.Views;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer
{
    public partial class App : Application
    {
        static DataAccess database;
        ProductoLiteRepository repoproducto;
        SolicitudLiteRepository reposolicitud;
        SolicitudDataFarmaciasLiteRepository reposolicitudfarma;

        public App()
        {
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIwNjc4QDMxMzcyZTM0MmUzMEdLN2NIeUV6dUE4Nnc0UGpCNGdHRndrVFVmeHljazlzdkxYcGVmZTJZUkk9");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjg0MTQyQDMxMzgyZTMyMmUzMFY3Nm9HLzZpUmxDeXRVZVZwT05uK1dsdVoxMER1QStsL0JhcGEvTzB6ZVU9");
            
            InitializeComponent();
            this.reposolicitud = new SolicitudLiteRepository();
            this.repoproducto = new ProductoLiteRepository();
            this.reposolicitudfarma = new SolicitudDataFarmaciasLiteRepository();
            this.reposolicitud.CrearBBDD();
            this.repoproducto.CrearBBDD();
            this.reposolicitudfarma.CrearBBDD();
            if (Settings.IsRemember)
            {
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                if (Settings.Expires > DateTime.Now)
                {

                    var mainViewModel = MenuTabbedPageViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.Usuario.Usuario = Settings.UserEmail;
                    mainViewModel.Usuario.Token = Settings.UserPassword;
                    App.Current.MainPage = new MenuTabbedPage();
                }
                else
                {
                    //MainPage = new LoginPage();
                    MainPage = new NavigationPage(new LoginPage());
                    this.BindingContext = new LoginPageViewModel();
                }
            }
            else
            {
                //MainPage = new LoginPage();
                MainPage = new NavigationPage(new LoginPage());
                this.BindingContext = new LoginPageViewModel();
            }
            
        }

        public static DataAccess Database
        {
            get
            {
                if (database == null)
                {
                    database = new DataAccess();
                }
                return database;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}

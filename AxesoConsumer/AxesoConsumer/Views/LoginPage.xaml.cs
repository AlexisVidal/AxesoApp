using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using AxesoConsumer.Services;
using AxesoConsumer.ViewModels;
using Newtonsoft.Json;
using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        //private IGoogleAuthService _googleAuthService;
        private ModelsBL usuarioBL = new ModelsBL();
        Usuarios entidad = new Usuarios();
        Account account;
        AccountStore store;

        public LoginPage()
        {
            InitializeComponent();
            try
            {
                this.BindingContext = new LoginPageViewModel();
                store = AccountStore.Create();
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnDisappearing()
        {
            this.BindingContext = new LoginPageViewModel();
            base.OnDisappearing();
        }
        protected override void OnAppearing()
        {
            this.BindingContext = new LoginPageViewModel();
            base.OnAppearing();
        }
    }
}
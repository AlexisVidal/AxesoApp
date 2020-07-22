using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using AxesoConsumer.ViewModels;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnvioExitosoPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        Usuarios entidad = new Usuarios();
        private ModelsBL usuarioBL = new ModelsBL();
        string email = Settings.UserEmail;
        string name = Settings.UserName;
        public EnvioExitosoPopUpPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
        }

        async void AcceptEnvio_Clicked(object sender, EventArgs e)
        {
            //var loadingPage = new LoadingPopupPage();
            //await PopupNavigation.PushAsync(loadingPage);
            var userexist = await CheckIfExistUser(email);
            var mainViewModel = MenuTabbedPageViewModel.GetInstance();
            mainViewModel.Usuario = entidad;
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            Settings.UserEmail = email;
            Settings.UserName = name;
            Settings.IsRemember = true;
            Settings.Expires = DateTime.Now.AddDays(1);
            Settings.UserPassword = "";

            //await Application.Current.MainPage.Navigation.PushAsync(new MenuTabbedPage());
            await PopupNavigation.PopAsync();
            
            //App.Current.MainPage = new MenuTabbedPage();
            //await PopupNavigation.RemovePageAsync(loadingPage);
        }
        public static INavigation Nav { get; set; }
        async Task<Usuarios> CheckIfExistUser(string email)
        {
            int contador = 0;
            try
            {
                var usuarios = await usuarioBL.Listar();
                if (usuarios.Any())
                {
                    contador = usuarios.Count();
                    var existe = usuarios.Where(x => x.Usuario.ToUpper().Equals(email.ToUpper())).FirstOrDefault();
                    return existe;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
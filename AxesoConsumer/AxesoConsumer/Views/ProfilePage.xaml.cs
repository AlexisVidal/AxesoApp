using AxesoConsumer.Helpers;
using AxesoConsumer.ViewModels;
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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            this.BindingContext = new ProfilePageViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var ingresoactual = await Task.Run(() => CommonFunctions.GetSetIngreso(Settings.UserID));
        }
        async void HistorialBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistorialComprasPage());
        }

        async void CambioPassBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePasswordPage());

        }

        async void CloseSessionBtn_Clicked(object sender, EventArgs e)
        {
            var closePage = new CloseSessionPage();
            await PopupNavigation.Instance.PushAsync(closePage);

        }

        async void TerminosBtn_Clicked(object sender, EventArgs e)
        {
            var termsPage = new TerminosPage();
            await PopupNavigation.Instance.PushAsync(termsPage);

        }

        async void DireccionesBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DireccionesPage());
        }
    }
}
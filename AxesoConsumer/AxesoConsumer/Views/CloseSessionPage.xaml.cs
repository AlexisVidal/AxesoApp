using AxesoConsumer.Helpers;
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
    public partial class CloseSessionPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public CloseSessionPage()
        {
            InitializeComponent();
        }

        async void SalirBtn_Clicked(object sender, EventArgs e)
        {
            Settings.Expires = DateTime.Now;
            Settings.IsRemember = false;
            await PopupNavigation.PopAsync();
            (Application.Current).MainPage = new NavigationPage(new LoginPage());
        }

        async void CancelaBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}
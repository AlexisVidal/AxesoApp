using AxesoConsumer.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErroPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        string error = "";
        public ErroPopUpPage()
        {
            InitializeComponent();
            error = Settings.ErrorText;
            lErrore.Text = error;
        }

        async void AcceptError_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}
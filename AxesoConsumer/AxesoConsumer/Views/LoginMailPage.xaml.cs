using Axeso_BL;
using AxesoConsumer.ViewModels;
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
    public partial class LoginMail : ContentPage
    {
        private ModelsBL usuarioBL = new ModelsBL();
        public LoginMail()
        {
            InitializeComponent();
            this.BindingContext = new LoginMailPageViewModel();
        }

        async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
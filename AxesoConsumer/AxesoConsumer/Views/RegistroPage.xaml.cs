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
    public partial class RegistroPage : ContentPage
    {
        public RegistroPage()
        {
            InitializeComponent();
            this.BindingContext = new RegistroPageViewModel();
        }

        async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
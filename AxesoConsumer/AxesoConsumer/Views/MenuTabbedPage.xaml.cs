using AxesoConsumer.ViewModels;
using Plugin.Badge.Abstractions;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuTabbedPage : TabbedPage
    {
        public MenuTabbedPage()
        {
            //este es un comentario
            try
            {
                InitializeComponent();
                this.BindingContext = new MenuTabbedPageViewModel();
            }
            catch (Exception ex)
            {
                var x = 1;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            string currentPageName = CurrentPage.Title;
            if (currentPageName.Equals("Carrito"))
            {
                NavigationPage naviPage = this.Children[1] as NavigationPage;
                CarritoPage page = naviPage.RootPage as CarritoPage;
                page.CargaTodo();
            }

        }
    }
}
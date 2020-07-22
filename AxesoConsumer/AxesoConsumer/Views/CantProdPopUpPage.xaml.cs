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
    public partial class CantProdPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        int cantidad = 0;
        string scant = "";
        public CantProdPopUpPage(string imagen, string mensaje, string mensaje2, string mensaje3)
        {
            InitializeComponent();
            imageCantResult.Source = imagen;
            lmensaje.Text = mensaje;
            lmensaje2.Text = mensaje2;
            lmensaje3.Text = mensaje3;
            if (mensaje2.Equals(""))
            {
                lmensaje2.IsVisible = false;
            }
            if (mensaje3.Equals(""))
            {
                lmensaje3.IsVisible = false;
            }
            //cantidad = Convert.ToInt32(Settings.CantResultado);
            //if (cantidad > 100)
            //{
            //    scant = cantidad.ToString() + " - solo se mostrará 100";
            //}
            //else
            //{
            //    scant = cantidad.ToString();
            //}
            //lCantProd.Text = scant;
        }

        async void AcceptCantProd_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}
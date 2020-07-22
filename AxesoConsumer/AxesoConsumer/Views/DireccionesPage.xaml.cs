using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
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
    public partial class DireccionesPage : ContentPage
    {
        ModelsBL modelb = new ModelsBL();
        public List<UsuarioDireccion> Items;
        int usuarioID = Settings.UserID;
        public DireccionesPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DireccionesItems.ItemsSource = null;
            var Itemsx = Task.Run(async () => await modelb.GetAllUsuarioDireccionByUsuarioID(usuarioID)).Result;
            if (Itemsx.Any())
            {
                Items = (List<UsuarioDireccion>)Itemsx.ToList();
                DireccionesItems.ItemsSource = Items;
            }
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        async void btnModificaDireccion_Clicked(object sender, EventArgs e)
        {
            var args = (ImageButton)sender;
            var usuariodireccionid = args.CommandParameter.ToString();
            if (Convert.ToInt32(usuariodireccionid) <= 0)
            {
                return;
            }
            int usuariodirecid = Convert.ToInt32(usuariodireccionid);
            Settings.UsuarioDireccionID = usuariodirecid;
            var entidadID = Items.Where(x=>x.UsuarioDireccionID == usuariodirecid).FirstOrDefault();
            if (entidadID != null)
            {
                
            }
            else
            {
                return;
            }
            await Navigation.PushAsync(new DireccionDetailPage());
        }

        async void btnAgregaDireccion_Clicked(object sender, EventArgs e)
        {
            int usuariodirecid = Convert.ToInt32(0);
            Settings.UsuarioDireccionID = usuariodirecid;
            await Navigation.PushAsync(new DireccionDetailPage());
        }
    }
}
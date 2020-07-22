using Axeso_BE;
using Axeso_BL;
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
    public partial class SolicitudDatabasePage : ContentPage
    {
        private ModelsBL modelBl = new ModelsBL();
        int currentSolicitudID = 0;
        Solicitud solicitud;
        List<SolicitudProducto> lproducto;
        List<SolicitudDataFarmacias> lsolicitudfarmacias;
        int currentUsuarioID = 0;
        public SolicitudDatabasePage()
        {
            InitializeComponent();
            currentUsuarioID = Settings.UserID;
            currentSolicitudID = Settings.CurrentSolicitudID;
            solicitud = new Solicitud();
            lproducto = new List<SolicitudProducto>();
            lsolicitudfarmacias = new List<SolicitudDataFarmacias>();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                ProductoItems.ItemsSource = null;
            }
            catch (Exception ex)
            {

            }
            await LoadSolicitud();
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        async Task LoadSolicitud()
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            try
            {
                var lsolicitud = await modelBl.GetAllSolicitudesByUsuarioID(currentUsuarioID, currentSolicitudID);
                if (lsolicitud.Any())
                {
                    solicitud = lsolicitud.FirstOrDefault();
                    AddressLabel.Text = solicitud.Direccion;
                    lproducto = (List<SolicitudProducto>) await modelBl.GetAllSolicitudesProductoByUsuarioID(currentUsuarioID, currentSolicitudID);
                    if (lproducto.Any())
                    {
                        ProductoItems.ItemsSource = lproducto;
                    }
                    else
                    {
                        ProductoItems.ItemsSource = null;
                    }
                    lsolicitudfarmacias = (List<SolicitudDataFarmacias>) await modelBl.GetAllSolicitudDataFarmaciasBySolicitudID(currentSolicitudID);
                    if (lsolicitudfarmacias.Any())
                    {
                        ldatafarma.Text = "La solicitud se envio a " + lsolicitudfarmacias.Count + " farmacias";
                    }
                }

                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
            }
        }
    }
}
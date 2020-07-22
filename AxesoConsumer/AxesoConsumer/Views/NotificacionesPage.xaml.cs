using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificacionesPage : ContentPage
    {
        ModelsBL modelbl = new ModelsBL();
        int usuarioid = Settings.UserID;
        private static bool banderaClick = true;
        List<Notificacion> listNotificacion = new List<Notificacion>();
        CultureInfo ci = new CultureInfo("es-PE");
        bool enabledcoti = true;
        public NotificacionesPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var ingresoactual = await Task.Run(() => CommonFunctions.GetSetIngreso(Settings.UserID));
            banderaClick = true;
            try
            {
                enabledcoti = true;
                listViewNotificaciones.ItemsSource = null;
                LlenarNotificaciones();
            }
            catch (Exception ex)
            {

            }
        }

        async void LlenarNotificaciones()
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            var lista = await modelbl.GetAllNotificacionByUsuario(usuarioid);
            if (lista != null)
            {
                if (lista.Any())
                {
                    listNotificacion = (List<Notificacion>)lista.OrderByDescending(x => x.Fecha).ToList();
                    listViewNotificaciones.ItemsSource = null;
                    listViewNotificaciones.ItemsSource = listNotificacion.OrderByDescending(x => x.Fecha).ToList();
                    listViewNotificaciones.ItemSelected += ListViewNotificaciones_ItemSelected;
                }
            }

            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        private void ListViewNotificaciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}
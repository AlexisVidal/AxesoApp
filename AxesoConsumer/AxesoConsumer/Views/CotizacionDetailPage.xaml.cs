using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CotizacionDetailPage : ContentPage
    {
        ModelsBL modelbl = new ModelsBL();
        int CotizacionID = 0;
        public IEnumerable<UsuarioDireccion> ItemsDireccion;
        int usuarioid = Settings.UserID;
        public List<UsuarioDireccionTemp> ItemsDireccionTemp;
        int userdirecselected = Settings.UsuarioDireccionID;
        List<CotizacionProducto> detalles = new List<CotizacionProducto>();
        Cotizacion cotizacion = new Cotizacion();
        public CotizacionDetailPage()
        {
            InitializeComponent();
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var ingresoactual = await Task.Run(() => CommonFunctions.GetSetIngreso(Settings.UserID));
            CultureInfo myCurrency = new CultureInfo("es-PE");
            CultureInfo.DefaultThreadCurrentCulture = myCurrency;
            CotizacionID = Convert.ToInt32(Settings.CotizacionID);
            LoadData();
        }

        async void LoadData()
        {
            try
            {
                cotizacion = new Cotizacion();
                var cotixa = await modelbl.GetAllCotizacionById(CotizacionID);
                if (cotixa != null)
                {
                    cotizacion = (Cotizacion)cotixa;
                    var objetoserializado = JsonConvert.SerializeObject(cotizacion, Formatting.Indented);
                    Settings.CotizacionJson = objetoserializado;
                    var pedidox = await modelbl.GetAllPedidosBySolicitud(cotizacion.SolicitudID);
                    if (pedidox.Any())
                    {
                        btnPedirCotizacion.IsVisible = false;
                        btnCancelarCotizacion.IsVisible = false;
                    }
                    else
                    {
                        btnPedirCotizacion.IsVisible = true;
                        btnCancelarCotizacion.IsVisible = true;
                    }
                }
                detalles = new List<CotizacionProducto>();
                var xdetalles = await modelbl.GetAllCotizacionProductosByCotizacionID(CotizacionID);
                if (xdetalles.Any())
                {
                    detalles = (List<CotizacionProducto>)xdetalles.ToList();
                    FarmaLabel.Text = detalles.FirstOrDefault().Cotizacion.Nombre;
                    FarmaDireccionLabel.Text = detalles.FirstOrDefault().Cotizacion.Direccion;
                    CotizacionItems.ItemsSource = detalles;
                    lcproductos.Text = xdetalles.Count().ToString();
                    ltproductos.Text = xdetalles.Sum(x => x.PrecioTotal).ToString("0.#0");
                    lstproductos.Text = xdetalles.Sum(x => x.PrecioTotal).ToString("0.#0");
                    FarmaSolicDireccion.Text = detalles.FirstOrDefault().Cotizacion.Solicitud.Direccion;
                }
                else
                {
                    CotizacionItems.ItemsSource = null;
                }
                
                
            }
            catch (Exception ex)
            {
                CotizacionItems.ItemsSource = null;
            }
        }

        async void btnPedirCotizacion_Clicked(object sender, EventArgs e)
        {
            if (CotizacionID > 0)
            {
                var listadoserializado = JsonConvert.SerializeObject(detalles, Formatting.Indented);
                Settings.CotizacionProductoJson = listadoserializado;
                await Navigation.PushAsync(new ConfirmaCotizacionPage());
            }
        }

        async void btnCancelarCotizacion_Clicked(object sender, EventArgs e)
        {
            if (CotizacionID > 0)
            {
                var loadingPage = new LoadingPopupPage();
                await PopupNavigation.Instance.PushAsync(loadingPage);

                try
                {
                    Cotizacion forupdate = new Cotizacion()
                    {
                        CotizacionID = CotizacionID,
                        Estado = "3"
                    };
                    var resultado = await modelbl.UpdateCotizacion(forupdate);
                    if (resultado != null)
                    {
                        await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                        Settings.SuccessText = "Registro exito";
                        await PopupNavigation.Instance.PushAsync(new SuccessPopUpPage());
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                        Settings.ErrorText = "Hubo un error al rechazar!";
                        await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                        return;
                    }
                }
                catch (Exception ex)
                {
                    await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                }
                
            }
        }
    }
}
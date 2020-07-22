using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using MvvmHelpers;
using Syncfusion.SfRangeSlider.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeguimientoTabbedPage : TabbedPage
    {
        ModelsBL modelbl = new ModelsBL();
        int usuarioid = Settings.UserID;
        private static bool banderaClick = true;
        Solicitud solicitudselected = new Solicitud();
        List<SolicitudList> listasolicitud = new List<SolicitudList>();
        List<Solicitud> listasolicitudexs = new List<Solicitud>();
        List<CotizacionList> listaCotizacionList = new List<CotizacionList>();
        public List<CotizacionListGroup> CotizacionListGrou = new List<CotizacionListGroup>();
        //public ObservableCollection<Grouping<string, CotizacionList>> Items { get; set; } = new ObservableCollection<Grouping<string, CotizacionList>>();
        List<CotizacionProducto> cotiprod = new List<CotizacionProducto>();
        bool enabledcoti = true;
        public SeguimientoTabbedPage()
        {
            try
            {
                InitializeComponent();

            }
            catch (Exception ex)
            {

            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            banderaClick = true;
            try
            {
                enabledcoti = true;
                LlenarSolicitudes();
                //LlenarCotizaciones();
                //await Task.Yield();
            }
            catch (Exception ex)
            {

            }
        }

        private void ListViewCotizaciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
        public ObservableCollection<Grouping<string, CotizacionList>> Items { get; set; } = new ObservableCollection<Grouping<string, CotizacionList>>();
        async void LlenarSolicitudes()
        {

            Solicitud lsolicitudes = new Solicitud();
            listasolicitud = new List<SolicitudList>();
            listViewSolicitudes.ItemsSource = null;
            var listasolicitudes = await modelbl.GetAllSolicitudesByUsuario(usuarioid);
            if (listasolicitudes.Any())
            {
                listasolicitudexs = (List<Solicitud>)listasolicitudes.Where(x => x.Activo).OrderBy(z => z.Fecha).ToList();
                int nroitems = 1;
                foreach (var item in listasolicitudes.Where(x => x.Activo).OrderBy(z => z.Fecha).ToList())
                {
                    int cant = 0;
                    if (item.SolicitudProducto != null)
                    {
                        cant = item.SolicitudProducto.Count;
                    }
                    SolicitudList nueva = new SolicitudList()
                    {
                        NroItem = nroitems,
                        SolicitudID = item.SolicitudID,
                        FechaEnviado = item.FechaEnviado,
                        TotalProductos = cant.ToString() + " Productos",
                        Cotizado = item.Cotizado
                    };
                    listasolicitud.Add(nueva);
                    nroitems++;
                }
            }
            listViewSolicitudes.ItemsSource = listasolicitud.ToList();
            listViewSolicitudes.ItemSelected += ListViewSolicitudes_ItemSelected;


            #region Cotizaciones
            CotizacionList cotizacionlist = new CotizacionList();
            listaCotizacionList = new List<CotizacionList>();
            listViewCotizaciones.ItemsSource = null;
            var listacotizacion = await modelbl.GetAllCotizacionesByUsuarioID(usuarioid);

            if (listacotizacion.Any())
            {
                var listasolicitudx = (List<SolicitudList>)listasolicitud;
                // var groups = listacotizacion.GroupBy(x => x.SolicitudID);
                foreach (var item in listacotizacion.Where(x => x.Activo).OrderBy(z => z.Fecha).ToList())
                {
                    int numitem = listasolicitudx.Where(x => x.SolicitudID == item.Solicitud.SolicitudID).Select(q => q.NroItem).First();
                    int idcotizacion = item.CotizacionID;
                    cotiprod = (List<CotizacionProducto>)await modelbl.GetAllCotizacionProductosByCotizacionID(idcotizacion);
                    int canttotalpro = 0;
                    int canttotaltake = 0;
                    decimal total = 0;
                    if (cotiprod.Any())
                    {
                        var resul = (List<SolicitudProducto>) await modelbl.GetAllSolicitudesProductoByUsuarioID(usuarioid, item.SolicitudID);
                        canttotalpro = resul.Select(x => x.ProductoID).ToList().Count;
                        canttotaltake = cotiprod.Count;
                        total = cotiprod.Sum(x => x.PrecioTotal);
                    }
                    string titulox = "Lista " + numitem.ToString();
                    cotizacionlist = new CotizacionList()
                    {
                        CotizacionID = item.CotizacionID,
                        UsuarioID = item.UsuarioID,
                        SolicitudID = item.SolicitudID,
                        Activo = item.Activo,
                        Fecha = item.Fecha,
                        FechaGenerado = item.FechaGenerado,
                        Titulo = titulox,
                        FarmaciaNombre = item.Nombre,
                        Distancia = " Km aprox.",
                        CantProductos = canttotalpro,
                        CantProductosTake = canttotaltake,
                        CantProductosString = canttotaltake.ToString() + "/" + canttotalpro.ToString() + " Productos",
                        Total = "S/ " + total.ToString("F"),
                        Nombre = item.Nombre,
                        Direccion = item.Direccion,
                        Latitud = item.Latitud,
                        Longitud = item.Longitud
                    };
                    listaCotizacionList.Add(cotizacionlist);
                }
                var itemss = from lista in listaCotizacionList
                             orderby lista.Titulo
                             group lista by lista.Titulo into listagroup
                             select new Grouping<string, CotizacionList>(listagroup.Key, listagroup);
                if (Items.Any())
                {
                    foreach (var item in Items.Where(x => x.Count >0).ToList())
                    {
                        Items.Remove(item);
                    }
                }
                foreach (var xitem in itemss)
                {
                    Items.Add(xitem);
                }
            }

            listViewCotizaciones.ItemsSource = null;

            listViewCotizaciones.ItemsSource = Items.ToList();
            listViewCotizaciones.ItemSelected += ListViewCotizaciones_ItemSelected;
            #endregion
        }

        

        async void ListViewSolicitudes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (banderaClick)
            {
                banderaClick = false;
                var item = e.SelectedItem as SolicitudList;
                if ((item != null))
                {
                    var oSeleccionado = item.SolicitudID;
                    banderaClick = false;
                    Settings.CurrentSolicitudID = oSeleccionado;
                    await Navigation.PushAsync(new SolicitudDatabasePage());
                }
            } // fin banderaCLick
        }

        private async void btnUbicacion_Clicked(object sender, EventArgs e)
        {
            var args = (Button)sender;
            var dataFarmaciasID = args.CommandParameter.ToString();
            if (Convert.ToInt32(dataFarmaciasID) <= 0)
            {
                return;
            }
            var datafarma = cotiprod.Where(x => x.Cotizacion.UsuarioID == Convert.ToInt32(dataFarmaciasID)).Select(z => z.Cotizacion).FirstOrDefault();
            if (datafarma != null)
            {
                var location = new Xamarin.Essentials.Location(datafarma.Latitud, datafarma.Longitud);
                var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };
                await Map.OpenAsync(location, options);
            }
        }

        async void CotizacionGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (enabledcoti)
            {
                enabledcoti = false;
                var view = sender as View;
                var cotix = view?.BindingContext as CotizacionList;
                if (cotix != null)
                {
                    Settings.CotizacionID = Convert.ToInt32(cotix.CotizacionID);
                    if (Convert.ToInt32(cotix.CotizacionID) > 0)
                    {
                        await Navigation.PushAsync(new CotizacionDetailPage());
                    }
                    else
                    {

                    }
                }
            }
        }
       
    }
}
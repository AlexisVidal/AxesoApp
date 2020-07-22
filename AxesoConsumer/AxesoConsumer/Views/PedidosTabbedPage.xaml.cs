using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using GeoCoordinatePortable;
using MvvmHelpers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidosTabbedPage : TabbedPage
    {
        ModelsBL modelbl = new ModelsBL();
        int usuarioid = Settings.UserID;
        private static bool banderaClick = true;
        Solicitud solicitudselected = new Solicitud();
        List<SolicitudList> listasolicitud = new List<SolicitudList>();
        List<Solicitud> listasolicitudexs = new List<Solicitud>();
        List<CotizacionList> listaCotizacionList = new List<CotizacionList>();
        public List<CotizacionListGroup> CotizacionListGrou = new List<CotizacionListGroup>();
        List<CotizacionProducto> cotiprod = new List<CotizacionProducto>();
        CultureInfo ci = new CultureInfo("es-PE");
        bool enabledcoti = true;
        public PedidosTabbedPage()
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
                LlenarSolicitudes();
            }
            catch (Exception ex)
            {

            }
        }
        public ObservableCollection<Grouping<string, CotizacionList>> Items { get; set; } = new ObservableCollection<Grouping<string, CotizacionList>>();
        async void LlenarSolicitudes()
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            List<Cotizacion> listacotizacion = new List<Cotizacion>();
            List<Pedido> listaPedido = new List<Pedido>();
            List<PedidoList> listaPedidoList = new List<PedidoList>();


            Solicitud lsolicitudes = new Solicitud();
            listasolicitud = new List<SolicitudList>();
            listViewSolicitudes.ItemsSource = null;
            listViewSolicitudesOld.ItemsSource = null;
            var listasolicitudes = await modelbl.GetAllSolicitudesByUsuario(usuarioid);
            if (listasolicitudes.Any())
            {
                listasolicitudexs = (List<Solicitud>)listasolicitudes.Where(x => x.Activo).OrderBy(z => z.Fecha).ToList();
                int nroitems = 1;
                foreach (var item in listasolicitudes.Where(x => x.Activo).OrderBy(z => z.Fecha).ToList())
                {
                    int cant = 0;
                    var resul = (List<SolicitudProducto>)await modelbl.GetAllSolicitudesProductoByUsuarioID(usuarioid, item.SolicitudID);
                    if (resul != null)
                    {
                        cant = resul.Count;
                    }
                    string color = "#98CA3F";
                    string scotizado = "Cotizada";
                    if (!item.Cotizado)
                    {
                        color = "#808080";
                        scotizado = "Enviada";
                    }
                    decimal total = resul.Sum(x=>x.PrecioTotal);
                    SolicitudList nueva = new SolicitudList()
                    {
                        NroItem = nroitems,
                        SolicitudID = item.SolicitudID,
                        Fecha = item.Fecha,
                        FechaEnviado = item.FechaEnviado,
                        TotalProductos = cant.ToString() + " Productos",
                        Cotizado = item.Cotizado,
                        Color = color,
                        PrecioTotal = total,
                        SCotizado = scotizado
                    };
                    listasolicitud.Add(nueva);
                    nroitems++;
                    var cotix = await modelbl.GetAllCotizacionBySolicitud(item.SolicitudID);
                    if (cotix != null)
                    {
                        foreach (var itemy in cotix)
                        {
                            listacotizacion.Add(itemy);
                        }
                        
                    }
                    var pedidox = await modelbl.GetAllPedidosBySolicitud(item.SolicitudID);
                    if (pedidox != null)
                    {
                        foreach (var itemyz in pedidox)
                        {
                            listaPedido.Add(itemyz);
                        }

                    }
                }
            }
            
            double firsth = 100;
            if (listasolicitud.Where(x => x.Fecha.Date == DateTime.Now.Date).ToList().Count > 0)
            {
                firsth = (10 * listasolicitud.Where(x => x.Fecha.Date == DateTime.Now.Date).ToList().Count);
            }
            listViewSolicitudes.ItemsSource = null;
            listViewSolicitudes.ItemsSource = listasolicitud.Where(x => x.Fecha.Date == DateTime.Now.Date).OrderByDescending(x => x.NroItem).ToList();
            listViewSolicitudes.HeightRequest = firsth; 
            listViewSolicitudes.ItemSelected += ListViewSolicitudes_ItemSelected;
            double twoh = 100;
            if (listasolicitud.Where(x => x.Fecha.Date < DateTime.Now.Date).ToList().Count > 0)
            {
                twoh = (100 * listasolicitud.Where(x => x.Fecha.Date == DateTime.Now.Date).ToList().Count);
            }
            listViewSolicitudesOld.ItemsSource = null;
            listViewSolicitudesOld.ItemsSource = listasolicitud.Where(x => x.Fecha.Date < DateTime.Now.Date).OrderByDescending(x => x.NroItem).ToList();
            listViewSolicitudesOld.HeightRequest = twoh;
            listViewSolicitudesOld.ItemSelected += ListViewSolicitudes_ItemSelected;
            listViewSolicitudesOld.ItemSelected += ListViewSolicitudes_ItemSelected;
            listViewSolicitudesOld.ItemSelected += ListViewSolicitudes_ItemSelected;
            
            #region Pedidos
            listViewCotizaciones.ItemsSource = null;
            listViewCotizacionesOld.ItemsSource = null;

            if (listaPedido.Any())
            {
                foreach (var itempedi in listaPedido)
                {
                    int idpedido = itempedi.PedidoID;
                    var productsx = await modelbl.GetAllPedidoProductoByPedido(idpedido);
                    int canttotalpro = 0;
                    int canttotaltake = 0;
                    decimal total = 0;
                    if (productsx.Any())
                    {
                        //var resul = (List<PedidoProducto>)await modelbl.GetAllSolicitudesProductoByUsuarioID(usuarioid, item.SolicitudID);
                        canttotalpro = productsx.Select(x => x.ProductoID).ToList().Count;
                        canttotaltake = canttotalpro;
                        total = itempedi.TotalPagar;
                    }
                    string titulox = "Pedido " + itempedi.Numero.ToString();

                    GeoCoordinate pin1 = new GeoCoordinate(itempedi.LatitudEntrega, itempedi.LongitudEntrega);
                    GeoCoordinate pin2 = new GeoCoordinate(itempedi.Latitud, itempedi.Longitud);
                    double distanceBetween = (pin1.GetDistanceTo(pin2)) / 1000;
                    double aTruncated = Math.Truncate(distanceBetween * 100) / 100;
                    string colorpedido = "#98CA3F";
                    string scolorpedido = "| Entregado";
                    if (itempedi.Estado.Equals("1"))
                    {
                        colorpedido = "#808080";
                        scolorpedido = "| Recibido";
                    }
                    else if (itempedi.Estado.Equals("2"))
                    {
                        colorpedido = "#F6C800";
                        scolorpedido = "| Preparando";
                    }
                    else if (itempedi.Estado.Equals("3"))
                    {
                        colorpedido = "#2B83B2";
                        scolorpedido = "| Enviado";
                    }
                    else if (itempedi.Estado.Equals("4"))
                    {
                        colorpedido = "#3CB371";
                        scolorpedido = "| Entregado";
                    }
                    else if (itempedi.Estado.Equals("5"))
                    {
                        colorpedido = "#EB002A";
                        scolorpedido = "| Rechazado";
                    }
                    PedidoList newpedidolist = new PedidoList()
                    {
                        PedidoID = itempedi.PedidoID,
                        Numero = itempedi.Numero,
                        CotizacionID = itempedi.CotizacionID,
                        UsuarioID = itempedi.UsuarioID,
                        SolicitudID = itempedi.SolicitudID,
                        Activo = itempedi.Activo,
                        Fecha = itempedi.Fecha,
                        FechaGenerado = itempedi.FechaGenerado,
                        Nombre = itempedi.Nombre,
                        Direccion = itempedi.Direccion,
                        Latitud = itempedi.Latitud,
                        Longitud = itempedi.Longitud,
                        Estado = itempedi.Estado,
                        NombreEntrega = itempedi.NombreEntrega,
                        DireccionEntrega = itempedi.DireccionEntrega,
                        LatitudEntrega = itempedi.LatitudEntrega,
                        LongitudEntrega = itempedi.LongitudEntrega,
                        TotalPagar = itempedi.TotalPagar,
                        TipoPagar = itempedi.TipoPagar,
                        MontoPagar = itempedi.MontoPagar,
                        Titulo = titulox,
                        SEstado = scolorpedido,
                        Color = colorpedido,

                        Distancia = string.Format(ci, "{0:0.00}", aTruncated) + " Km aprox.",
                        CantProductos = canttotalpro,
                        CantProductosTake = canttotaltake,
                        CantProductosString = canttotalpro.ToString() + " Productos",
                        Total = "S/ " + string.Format(ci, "{0:0.00}", total) // total.ToString("F"),                        
                    };
                    listaPedidoList.Add(newpedidolist);
                }

                double firstc = 100;
                if (listaPedidoList.Where(x => x.Estado.Equals("1") || x.Estado.Equals("2") || x.Estado.Equals("3")).ToList().Count == 100)
                {
                    firstc = 100;
                }
                else if (listaPedidoList.Where(x => x.Estado.Equals("1") || x.Estado.Equals("2") || x.Estado.Equals("3")).ToList().Count > 0)
                {
                    firstc = (100 * listaPedidoList.Where(x => x.Estado.Equals("1") || x.Estado.Equals("2") || x.Estado.Equals("3")).ToList().Count);
                }
                listViewCotizaciones.ItemsSource = null;
                listViewCotizaciones.ItemsSource = listaPedidoList.Where(x => x.Estado.Equals("1") || x.Estado.Equals("2") || x.Estado.Equals("3")).OrderBy(x => x.PedidoID).ToList();
                listViewCotizaciones.HeightRequest = firstc;
                listViewCotizaciones.ItemSelected += ListViewCotizaciones_ItemSelected;
                double twoc = 100;
                if (listaPedidoList.Where(x => x.Estado.Equals("4") || x.Estado.Equals("5")).ToList().Count == 1)
                {
                    twoc = 100;
                }
                else if (listaPedidoList.Where(x => x.Estado.Equals("4") || x.Estado.Equals("5")).ToList().Count > 0)
                {
                    twoc = (100 * listaPedidoList.Where(x => x.Estado.Equals("4") || x.Estado.Equals("5")).ToList().Count);
                }
                listViewCotizacionesOld.ItemsSource = null;
                listViewCotizacionesOld.ItemsSource = listaPedidoList.Where(x => x.Estado.Equals("4") || x.Estado.Equals("5")).OrderBy(x => x.PedidoID).ToList();
                listViewCotizacionesOld.HeightRequest = twoc;
                listViewCotizacionesOld.ItemSelected += ListViewCotizaciones_ItemSelected;


            }
            #endregion


            #region Cotizaciones
            CotizacionList cotizacionlist = new CotizacionList();
            listaCotizacionList = new List<CotizacionList>();
            //listViewCotizaciones.ItemsSource = null;
            //var listacotizacion = await modelbl.GetAllCotizacionesByUsuarioID(usuarioid);
            
            //if (listacotizacion.Any())
            //{
            //    var listasolicitudx = (List<SolicitudList>)listasolicitud;
            //    // var groups = listacotizacion.GroupBy(x => x.SolicitudID);
            //    foreach (var item in listacotizacion.Where(x => x.Activo).OrderBy(z => z.Fecha).ToList())
            //    {
            //        int numitem = listasolicitudx.Where(x => x.SolicitudID == item.Solicitud.SolicitudID).Select(q => q.NroItem).First();
            //        int idcotizacion = item.CotizacionID;
            //        cotiprod = (List<CotizacionProducto>)await modelbl.GetAllCotizacionProductosByCotizacionID(idcotizacion);
            //        int canttotalpro = 0;
            //        int canttotaltake = 0;
            //        decimal total = 0;
            //        if (cotiprod.Any())
            //        {
            //            var resul = (List<SolicitudProducto>)await modelbl.GetAllSolicitudesProductoByUsuarioID(usuarioid, item.SolicitudID);
            //            canttotalpro = resul.Select(x => x.ProductoID).ToList().Count;
            //            canttotaltake = cotiprod.Count;
            //            total = cotiprod.Sum(x => x.PrecioTotal);
            //        }
            //        string titulox = "Lista " + numitem.ToString();

            //        GeoCoordinate pin1 = new GeoCoordinate(item.Solicitud.Latitud, item.Solicitud.Longitud);
            //        GeoCoordinate pin2 = new GeoCoordinate(item.Latitud, item.Longitud);
            //        double distanceBetween = (pin1.GetDistanceTo(pin2)) / 1000;
            //        double aTruncated = Math.Truncate(distanceBetween * 100) / 100;

            //        cotizacionlist = new CotizacionList()
            //        {
            //            CotizacionID = item.CotizacionID,
            //            UsuarioID = item.UsuarioID,
            //            SolicitudID = item.SolicitudID,
            //            Activo = item.Activo,
            //            Fecha = item.Fecha,
            //            FechaGenerado = item.FechaGenerado,
            //            Titulo = titulox,
            //            FarmaciaNombre = item.Nombre,
            //            Distancia = string.Format(ci, "{0:0.00}", aTruncated) + " Km aprox.",
            //            CantProductos = canttotalpro,
            //            CantProductosTake = canttotaltake,
            //            CantProductosString = canttotaltake.ToString() + "/" + canttotalpro.ToString() + " Productos",
            //            Total = "S/ " + string.Format(ci, "{0:0.00}", total), // total.ToString("F"),
            //            DTotal = total,
            //            Nombre = item.Nombre,
            //            Direccion = item.Direccion,
            //            Latitud = item.Latitud,
            //            Longitud = item.Longitud
            //        };
            //        listaCotizacionList.Add(cotizacionlist);
            //    }
            //}

            //listViewCotizaciones.ItemsSource = null;

            //listViewCotizaciones.ItemsSource = listaCotizacionList.OrderBy(x => x.DTotal).ToList();
            //listViewCotizaciones.ItemSelected += ListViewCotizaciones_ItemSelected;
            #endregion


            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        private void ListViewSolicitudesOld_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (banderaClick)
            {
                banderaClick = false;
                var item = e.SelectedItem as CotizacionList;
                if ((item != null))
                {
                    var oSeleccionado = item;
                    banderaClick = false;
                    //Settings.CurrentSolicitudID = oSeleccionado;
                    //await Navigation.PushAsync(new SolicitudDatabasePage());
                }
            }
        }

        private void ListViewCotizaciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (banderaClick)
            {
                banderaClick = false;
                var item = e.SelectedItem as CotizacionList;
                if ((item != null))
                {
                    var oSeleccionado = item;
                    banderaClick = false;
                    //Settings.CurrentSolicitudID = oSeleccionado;
                    //await Navigation.PushAsync(new SolicitudDatabasePage());
                }
            }
        }

        async void ListViewSolicitudes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (enabledcoti)
            {
                enabledcoti = false;
                var item = e.SelectedItem as SolicitudList;
                if ((item != null))
                {
                    var oSeleccionado = item.SolicitudID;
                    banderaClick = false;
                    if (item.Cotizado)
                    {
                        var cotizaxs = await modelbl.GetAllCotizacionBySolicitud(item.SolicitudID);
                        if (cotizaxs.Any())
                        {
                            Settings.CotizacionID = Convert.ToInt32(cotizaxs.FirstOrDefault().CotizacionID);
                            if (Convert.ToInt32(cotizaxs.FirstOrDefault().CotizacionID) > 0)
                            {
                                await Navigation.PushAsync(new CotizacionDetailPage());
                            }
                            else
                            {

                            }
                        }
                        
                    }
                    else
                    {
                        Settings.CurrentSolicitudID = oSeleccionado;
                        await Navigation.PushAsync(new SolicitudDatabasePage());
                    }
                    
                }
            }
        }

        async void CotizacionGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (enabledcoti)
            {
                enabledcoti = false;
                var view = sender as View;
                var cotix = view?.BindingContext as PedidoList;
                if (cotix != null)
                {
                    Settings.PedidoID = Convert.ToInt32(cotix.PedidoID);
                    if (Convert.ToInt32(cotix.PedidoID) > 0)
                    {
                        await Navigation.PushAsync(new EstadoPedidoPage());
                    }
                    else
                    {

                    }
                }
            }
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

        private void btnPedir_Clicked(object sender, EventArgs e)
        {

        }

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (enabledcoti)
            {
                enabledcoti = false;
                var view = sender as View;
                var cotix = view?.BindingContext as PedidoList;
                if (cotix != null)
                {
                    Settings.PedidoID = Convert.ToInt32(cotix.PedidoID);
                    if (Convert.ToInt32(cotix.PedidoID) > 0)
                    {
                        await Navigation.PushAsync(new EstadoPedidoPage());
                    }
                    else
                    {

                    }
                }
            }
        }

        async void SolicitudGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (enabledcoti)
            {
                enabledcoti = false;
                var view = sender as View;
                var item = view?.BindingContext as SolicitudList;
                if (item != null)
                {
                    var oSeleccionado = item.SolicitudID;
                    banderaClick = false;
                    if (item.Cotizado)
                    {
                        var cotizaxs = await modelbl.GetAllCotizacionBySolicitud(item.SolicitudID);
                        if (cotizaxs.Any())
                        {
                            Settings.CotizacionID = Convert.ToInt32(cotizaxs.FirstOrDefault().CotizacionID);
                            if (Convert.ToInt32(cotizaxs.FirstOrDefault().CotizacionID) > 0)
                            {
                                await Navigation.PushAsync(new CotizacionDetailPage());
                            }
                            else
                            {

                            }
                        }

                    }
                    else
                    {
                        Settings.CurrentSolicitudID = oSeleccionado;
                        await Navigation.PushAsync(new SolicitudDatabasePage());
                    }
                }
            }
        }

        async void SolicitudOldGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (enabledcoti)
            {
                enabledcoti = false;
                var view = sender as View;
                var item = view?.BindingContext as SolicitudList;
                if (item != null)
                {
                    var oSeleccionado = item.SolicitudID;
                    banderaClick = false;
                    if (item.Cotizado)
                    {
                        var cotizaxs = await modelbl.GetAllCotizacionBySolicitud(item.SolicitudID);
                        if (cotizaxs.Any())
                        {
                            Settings.CotizacionID = Convert.ToInt32(cotizaxs.FirstOrDefault().CotizacionID);
                            if (Convert.ToInt32(cotizaxs.FirstOrDefault().CotizacionID) > 0)
                            {
                                await Navigation.PushAsync(new CotizacionDetailPage());
                            }
                            else
                            {

                            }
                        }

                    }
                    else
                    {
                        Settings.CurrentSolicitudID = oSeleccionado;
                        await Navigation.PushAsync(new SolicitudDatabasePage());
                    }
                }
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }

        private void imgbtnoldsolic_Clicked(object sender, EventArgs e)
        {

        }
    }
}
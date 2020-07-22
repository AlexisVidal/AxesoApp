using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using GeoCoordinatePortable;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeguimientoTabbedTwoPage : TabbedPage
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

        public SeguimientoTabbedTwoPage()
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
                listViewCotizaciones.ItemsSource = null;
                listViewCotizacionesOld.ItemsSource = null;
                listViewSolicitudes.ItemsSource = null;
                listViewSolicitudesOld.ItemsSource = null;
                LlenarSolicitudesEnCurso();
            }
            catch (Exception ex)
            {

            }
        }

        async void LlenarSolicitudesEnCurso()
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            List<Cotizacion> listacotizacion = new List<Cotizacion>();
            List<Pedido> listaPedido = new List<Pedido>();
            List<PedidoList> listaPedidoList = new List<PedidoList>();


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
                    decimal total = resul.Sum(x => x.PrecioTotal);
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
            List<SolicitudList> solicurso = new List<SolicitudList>();
            solicurso = listasolicitud.Where(x => x.Fecha.Date == DateTime.Now.Date).ToList();
            //solicurso = listasolicitud.ToList();
            if (solicurso.Count > 0)
            {
                firsth = (100 * solicurso.Count);
            }
            listViewSolicitudes.ItemsSource = null;
            if (solicurso.Any())
            {
                listViewSolicitudes.ItemsSource = solicurso.OrderByDescending(x => x.NroItem).ToList();
                listViewSolicitudes.HeightRequest = firsth;
                listViewSolicitudes.ItemSelected += ListViewSolicitudes_ItemSelected;
                listViewSolicitudes.IsVisible = true;
                NoItems.IsVisible = false;
            }
            else
            {
                listViewSolicitudes.IsVisible = false;
                NoItems.IsVisible = true;
            }

            List<SolicitudList> soliante = new List<SolicitudList>();
            soliante = listasolicitud.Where(x => x.Fecha.Date < DateTime.Now.Date).ToList();
            //soliante = listasolicitud.ToList();
            double twoh = 100;
            if (soliante.ToList().Count > 0)
            {
                twoh = (100 * soliante.ToList().Count);
            }
            listViewSolicitudesOld.ItemsSource = null;
            if (soliante.Any())
            {
                listViewSolicitudesOld.ItemsSource = soliante.OrderByDescending(x => x.NroItem).ToList();
                listViewSolicitudesOld.HeightRequest = twoh;
                listViewSolicitudesOld.ItemSelected += ListViewSolicitudesOld_ItemSelected;
                listViewSolicitudesOld.IsVisible = true;
                NoItems2.IsVisible = false;
            }
            else
            {
                listViewSolicitudesOld.IsVisible = false;
                NoItems2.IsVisible = true;
            }
            


            #region pedidos
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
                List<PedidoList> pedidocurso = new List<PedidoList>();
                pedidocurso = listaPedidoList.Where(x => x.Estado.Equals("1") || x.Estado.Equals("2") || x.Estado.Equals("3")).ToList();
                //pedidocurso = listaPedidoList.ToList();
                List<PedidoList> pedidoanterior = new List<PedidoList>();
                pedidoanterior = listaPedidoList.Where(x => x.Estado.Equals("4") || x.Estado.Equals("5")).ToList();

                if (pedidocurso.Count == 100)
                {
                    firstc = 100;
                }
                else if (pedidocurso.Count > 0)
                {
                    firstc = (100 * pedidocurso.ToList().Count);
                }
                listViewCotizaciones.ItemsSource = null;
                listViewCotizaciones.ItemsSource = pedidocurso.OrderBy(x => x.PedidoID).ToList();
                listViewCotizaciones.HeightRequest = firstc;
                listViewCotizaciones.ItemSelected += ListViewCotizaciones_ItemSelected;

                double twoc = 100;
                if (pedidoanterior.ToList().Count == 1)
                {
                    twoc = 100;
                }
                else if (pedidoanterior.ToList().Count > 0)
                {
                    twoc = (100 * pedidoanterior.ToList().Count);
                }
                listViewCotizacionesOld.ItemsSource = null;
                listViewCotizacionesOld.ItemsSource = pedidoanterior.OrderBy(x => x.PedidoID).ToList();
                listViewCotizacionesOld.HeightRequest = twoc;
                listViewCotizacionesOld.ItemSelected += ListViewCotizacionesOld_ItemSelected;
            }
            #endregion

            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        private void ListViewCotizacionesOld_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ListViewCotizaciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ListViewSolicitudesOld_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ListViewSolicitudes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
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

        async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
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

        async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
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

        async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
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
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using AxesoConsumer.Repositories;
using GeoCoordinatePortable;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarritoPage : ContentPage
    {
        SolicitudLite solicitud = new SolicitudLite();
        List<ProductoLite> lproducto = new List<ProductoLite>();
        ModelsBL modelb = new ModelsBL();
        public IEnumerable<UsuarioDireccion> ItemsDireccion;
        public List<UsuarioDireccionTemp> ItemsDireccionTemp;
        double distancia = 1000;
        int usuarioid = Settings.UserID;
        public List<Establecimiento> Items;
        public List<DataFarmacias> dataFarmacias;
        public IEnumerable<DataFarmacias> farmacias;
        UsuarioIngreso ingresoactual = new UsuarioIngreso();
        int userdirecselected = Settings.UsuarioDireccionID;
        CultureInfo ci = new CultureInfo("es-PE");
        
        double preciototal = 0;
        internal async void CargaTodo()
        {
            lbnPrecioSub.Text = "Sub total: S/. 0.00";
            lbnPrecioTot.Text = "Total: S/. 0.00";
            this.reposolicitud = new SolicitudLiteRepository();
            this.reposolicituddata = new SolicitudDataFarmaciasLiteRepository();
            this.repoproducto = new ProductoLiteRepository();
            enableds = true;
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            try
            {
                var todos = this.reposolicitud.GetSolicitudLite();
                currentSolicitud = Settings.CurrentSolicitud;
                solicitudid = Settings.CurrentSolicitudID;
                lfarmaciasonlinetemp = await Task.Run(() => CommonFunctions.GetFarmaciasOnline());
                ingresoactual = await Task.Run(() => CommonFunctions.GetSetIngreso(Settings.UserID));
                lparametros = await Task.Run(() => CommonFunctions.GetParametros());
                if (lparametros != null && lparametros.Any())
                {
                    var rango = lparametros.Where(x => x.Nombre.ToLower().Equals("rango")).FirstOrDefault();
                    if (rango != null)
                    {
                        distancia = rango.Valor;
                        Settings.Distancia = distancia;
                    }
                }
                farmacias = Task.Run(async () => await modelb.GetAllDataFarmacias()).Result;
                dataFarmacias = new List<DataFarmacias>();
                ItemsDireccion = Task.Run(async () => await modelb.GetAllUsuarioDireccionByUsuarioID(usuarioid)).Result;
                if (ItemsDireccion.Any())
                {
                    ItemsDireccionTemp = new List<UsuarioDireccionTemp>();
                    foreach (var item in ItemsDireccion)
                    {
                        ItemsDireccionTemp.Add(new UsuarioDireccionTemp
                        {
                            UsuarioDireccionID = item.UsuarioDireccionID,
                            UsuarioID = item.UsuarioID,
                            Nombre = item.Nombre,
                            Direccion = item.Direccion,
                            Latitud = item.Latitud,
                            Longitud = item.Longitud,
                            Activo = item.Activo
                        });
                    }
                    comboDirecciones.DataSource = null;
                    comboDirecciones.DisplayMemberPath = "Direccion";
                    comboDirecciones.SelectedValuePath = "UsuarioDireccionID";
                    comboDirecciones.DataSource = ItemsDireccionTemp;
                    comboDirecciones.SelectedValue = userdirecselected;
                }
                LoadSolicitud();
            }
            catch (Exception ex)
            {

            }


            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        List<UsuarioDireccion> lfarmaciasonlinetemp = new List<UsuarioDireccion>();
        List<UsuarioDireccion> lfarmaciasonline = new List<UsuarioDireccion>();
        int idubicacion = 0;
        bool enableds = true;
        List<Parametro> lparametros = new List<Parametro>();
        List<SolicitudDataFarmaciasLite> lsolicitudfarmacias = new List<SolicitudDataFarmaciasLite>();
        ProductoLiteRepository repoproducto;
        SolicitudLiteRepository reposolicitud;
        SolicitudDataFarmaciasLiteRepository reposolicituddata;
        string currentSolicitud = "";
        int solicitudid = 0;
        public CarritoPage()
        {
            InitializeComponent();
            CultureInfo.DefaultThreadCurrentCulture = ci;
        }

        
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            CargaTodo();
        }

        async void LoadSolicitud()
        {
            try
            {
                solicitud = this.reposolicitud.BuscarSolicitudLite(currentSolicitud);
                lproducto = this.repoproducto.GetProductoLiteBySolicitud(currentSolicitud);
                if (lproducto.Any())
                {
                    ProductoItems.ItemsSource = lproducto;
                    preciototal = Convert.ToDouble(lproducto.Sum(x => x.PrecioTotal));
                    lbnPrecioSub.Text = "Sub total: S/. " + string.Format(ci, "{0:0.00}", preciototal);
                    lbnPrecioTot.Text = "Total: S/. " + string.Format(ci, "{0:0.00}", preciototal);
                }
                else
                {
                    ProductoItems.ItemsSource = null;
                }

                lsolicitudfarmacias = this.reposolicituddata.BuscarSolicitudDataFarmaciasLitesByFk(currentSolicitud);

            }
            catch (Exception ex)
            {
                
            }
        }

        

        private void comboDirecciones_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var seleccion = (UsuarioDireccionTemp)e.Value;
            if (seleccion != null)
            {
                Settings.UsuarioDireccionID = seleccion.UsuarioDireccionID;
                Settings.Latitude = seleccion.Latitud;
                Settings.Longitude = seleccion.Longitud;
                Settings.Address = seleccion.Direccion;
                idubicacion = seleccion.UsuarioDireccionID;
                try
                {
                    comboFarmacias.DataSource = null;
                    lfarmaciasonline = new List<UsuarioDireccion>();
                }
                catch (Exception ex)
                {

                }
                if (lfarmaciasonlinetemp.Any())
                {
                    GeoCoordinate pin1 = new GeoCoordinate(Settings.Latitude, Settings.Longitude);
                    foreach (var item in lfarmaciasonlinetemp)
                    {
                        GeoCoordinate pin2 = new GeoCoordinate(item.Latitud, item.Longitud);
                        double distanceBetween = pin1.GetDistanceTo(pin2);
                        if (distanceBetween <= distancia)
                        {
                            lfarmaciasonline.Add(item);
                        }
                    }
                    comboFarmacias.Watermark = lfarmaciasonline.Count + " Farmacias cerca a ti...";
                    comboFarmacias.DataSource = null;
                    comboFarmacias.DisplayMemberPath = "Nombre";
                    comboFarmacias.SelectedValuePath = "UsuarioDireccionID";
                    comboFarmacias.DataSource = lfarmaciasonline;

                    var listadoserializado = JsonConvert.SerializeObject(lfarmaciasonline, Formatting.Indented);
                    Settings.FarmaciasOnlineList = listadoserializado;

                    try
                    {
                        var solicitudesdatalites = this.reposolicituddata.BuscarSolicitudDataFarmaciasLitesByFk(currentSolicitud);
                        if (solicitudesdatalites.Any())
                        {
                            this.reposolicituddata.EliminarAllSolicitudDataFarmaciasLite(solicitudesdatalites);
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        if (lfarmaciasonline.Any())
                        {
                            foreach (var item in lfarmaciasonline)
                            {
                                SolicitudDataFarmaciasLite nueva = new SolicitudDataFarmaciasLite()
                                {
                                    IdSolicitud = currentSolicitud,
                                    UsuarioID = item.UsuarioID,
                                    Activo = true
                                };
                                //listSolicitudFarma.Add(nueva);
                                this.reposolicituddata.InsertarSolicitudDataFarmaciasLite(nueva);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }


        async void btnViewFarmacias_Clicked(object sender, EventArgs e)
        {
            if (idubicacion == 0)
            {
                Settings.ErrorText = "Seleccione ubicacion";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
            if (enableds)
            {
                enableds = false;
                await Navigation.PushAsync(new FarmaciasOnlinePage(lfarmaciasonline));
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }


        async void btnEnviarSolicitud_Clicked(object sender, EventArgs e)
        {
            if (lproducto.Any())
            {
                var resultado = await EnviarSolicitud();
                if (resultado)
                {

                    this.reposolicitud.EnviarSolicitudLite(currentSolicitud, true);
                    try
                    {
                        this.repoproducto.EliminarListProductoLite(currentSolicitud);
                        this.reposolicitud.EliminarSolicitudLite(currentSolicitud);
                    }
                    catch (Exception ex)
                    {

                    }
                    await PopupNavigation.Instance.PushAsync(new EnvioExitosoPopUpPage());

                    try
                    {

                        App.Current.MainPage = new NavigationPage(new MenuTabbedPage());
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    Settings.ErrorText = "La solicitud no se pudo registrar!";
                    await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                    return;
                }
            }
            else
            {
                Settings.ErrorText = "La solicitud no tiene productos!";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
        }

        async Task<bool> EnviarSolicitud()
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            bool resultado = true;
            try
            {

                SolicitudInput entidadinsert = new SolicitudInput()
                {
                    SolicitudCode = solicitud.IdSolicitud,
                    UsuarioID = Settings.UserID,
                    Direccion = solicitud.Address,
                    Latitud = solicitud.Latitude,
                    Longitud = solicitud.Longitud,
                    Distancia = solicitud.Distancia,
                    Fecha = solicitud.Fecha,
                    Activo = solicitud.Activo,
                    FechaEnviado = "Enviado el " + DateTime.Now.ToString("dd/MM/yyyy") + " a las " + DateTime.Now.ToString("H:mm"),
                    Cotizado = false
                };
                var registro = await modelb.AddSolicitud(entidadinsert);
                if (registro != null)
                {
                    int cantidaditems = lproducto.Count();
                    int contador = 0;
                    int contador2 = 0;
                    int idinsertado = 0;
                    var insertado = await modelb.GetAllSolicitudesByUsuario(Settings.UserID);
                    if (insertado.Any())
                    {
                        idinsertado = insertado.Where(x => x.SolicitudCode.Equals(solicitud.IdSolicitud)).Select(z => z.SolicitudID).FirstOrDefault();
                    }
                    foreach (var item in lproducto)
                    {
                        SolicitudProductoInput detalleinsert = new SolicitudProductoInput()
                        {
                            SolicitudID = idinsertado,
                            ProductoLiteID = item.ProductoLiteID,
                            ProductoID = item.ProductoID,
                            TipoNegocioID = item.TipoNegocioID,
                            Nombre = item.Nombre,
                            Abreviatura = item.Abreviatura,
                            UnidadID = item.UnidadId,
                            UnidadNombre = item.UnidadNombre,
                            CategoriaID = item.CategoriaID,
                            CategoriaNombre = item.CategoriaNombre,
                            CategoriaAbreviatura = item.CategoriaAbreviatura,
                            Cantidad = item.Cantidad,
                            Activo = item.Activo,
                            Imagen = item.Imagen, 
                            PrecioUnitario = item.PrecioUnitario,
                            PrecioTotal = item.PrecioTotal,
                            RequiereReceta = item.RequiereReceta
                        };
                        var registrodetail = await modelb.AddSolicitudProducto(detalleinsert);
                        if (registrodetail != null)
                        {
                            contador += 1;
                        }
                    }

                    foreach (var itemsd in lsolicitudfarmacias)
                    {
                        SolicitudDataFarmaciasInput detallefarmas = new SolicitudDataFarmaciasInput()
                        {
                            SolicitudID = idinsertado,
                            UsuarioID = itemsd.UsuarioID,
                            Activo = true
                        };
                        var registrodetailfarmas = await modelb.AddSolicitudDataFarmacias(detallefarmas);
                        if (registrodetailfarmas != null)
                        {
                            contador2 += 1;
                        }
                    }

                    if (contador == cantidaditems)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }


                }
                else
                {
                    resultado = false;
                }
                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                return false;

            }
            return resultado;
        }

        async void btnSearchProducto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuscadorProductoPage());
        }
    }
}
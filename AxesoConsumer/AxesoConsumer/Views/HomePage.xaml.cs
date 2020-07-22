using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using AxesoConsumer.Repositories;
using GeoCoordinatePortable;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        ModelsBL modelb = new ModelsBL();
        public List<Categoria> Itemsc;
        public IEnumerable<ProductoMarca> ItemsMarcas;
        public IEnumerable<UsuarioDireccion> ItemsDireccion;
        public List<UsuarioDireccionTemp> ItemsDireccionTemp;
        double distancia = 1000;
        int usuarioid = Settings.UserID;
        public List<Establecimiento> Items;
        public List<DataFarmacias> dataFarmacias;
        public IEnumerable<DataFarmacias> farmacias;

        List<SolicitudLite> lsolicitudes = new List<SolicitudLite>();
        ProductoLiteRepository repoproducto;
        SolicitudLiteRepository reposolicitud;
        SolicitudDataFarmaciasLiteRepository reposolicituddata;
        UsuarioIngreso ingresoactual = new UsuarioIngreso();
        List<UsuarioDireccion> lfarmaciasonlinetemp = new List<UsuarioDireccion>();
        List<UsuarioDireccion> lfarmaciasonline = new List<UsuarioDireccion>();
        string location;
        int idubicacion = 0;
        bool enableds = true;
        List<Parametro> lparametros = new List<Parametro>();
        public HomePage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            enableds = true;
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);

            Settings.ProductoBuscado = "";
            searchBtn.Text = "";
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


            this.reposolicitud = new SolicitudLiteRepository();
            this.reposolicituddata = new SolicitudDataFarmaciasLiteRepository();
            this.repoproducto = new ProductoLiteRepository();
            LoadSolicitudes();
            farmacias = Task.Run(async () => await modelb.GetAllDataFarmacias()).Result;
            dataFarmacias = new List<DataFarmacias>();


            ListViewMarcas.ItemsSource = null;
            ItemsMarcas = Task.Run(async () => await modelb.ListarProductoMarca()).Result;
            if (ItemsMarcas.Any())
            {
                ListViewMarcas.ItemsSource = ItemsMarcas;
            }


            ListViewCategorias.ItemsSource = null;
            Itemsc = Task.Run(async () => await modelb.GetCategorias()).Result;
            if (Itemsc.Any())
            {
                ListViewCategorias.ItemsSource = Itemsc;
            }
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
            }

            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        private void LoadSolicitudes()
        {
            lsolicitudes = this.reposolicitud.BuscarSolicitudLitesByUser(Settings.UserEmail);
            //lsolicitudes = await App.Database.GetSolicitudes(Settings.UserEmail);

            if (lsolicitudes.Any())
            {
                //BuscarBtn.Text = "Continuar";
            }
            else
            {
                Settings.CurrentSolicitud = "";
            }
        }

        //async void RangeBtn_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new CoveragePage());
        //}

        //async void BuscarBtn_Clicked(object sender, EventArgs e)
        //{
        //    List<SolicitudDataFarmaciasLite> listSolicitudFarma = new List<SolicitudDataFarmaciasLite>();
        //    string textbtn = BuscarBtn.Text.ToLower();

        //    double distancias = Settings.Distancia;
        //    /*****/

        //    #region traefarmacias
        //    if (farmacias.Any())
        //    {
        //        GeoCoordinate pin1 = new GeoCoordinate(Settings.Latitude, Settings.Longitude);
        //        foreach (var item in farmacias)
        //        {
        //            GeoCoordinate pin2 = new GeoCoordinate(item.Latitud, item.Longitud);
        //            double distanceBetween = pin1.GetDistanceTo(pin2);
        //            if (distanceBetween <= distancias)
        //            {
        //                dataFarmacias.Add(item);
        //            }
        //        }

        //    }
        //    #endregion
        //    /********/

        //    if (idubicacion == 0 && textbtn.Equals("empezar"))
        //    {
        //        Settings.ErrorText = "Seleccione ubicacion";
        //        await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
        //        return;
        //    }


        //    string solicitudID = Constants.GeneraPass();
        //    if (textbtn.Equals("empezar"))
        //    {
        //        SolicitudLite solicito = new SolicitudLite
        //        {
        //            IdSolicitud = solicitudID,
        //            Usuario = Settings.UserEmail,
        //            Fecha = DateTime.Now,
        //            Activo = true,
        //            Latitude = Settings.Latitude,
        //            Longitud = Settings.Longitude,
        //            Address = Settings.Address,
        //            Distancia = Settings.Distancia,
        //            Enviado = false
        //        };
        //        //var resultado = await App.Database.InsertSolicitud(solicito);
        //        var resultado = this.reposolicitud.InsertarSolicitudLite(solicito);
        //        if (dataFarmacias.Any())
        //        {
        //            foreach (var item in dataFarmacias)
        //            {
        //                SolicitudDataFarmaciasLite nueva = new SolicitudDataFarmaciasLite()
        //                {
        //                    IdSolicitud = resultado.IdSolicitud,
        //                    DataFarmaciasID = item.DataFarmaciasID,
        //                    Activo = true
        //                };
        //                listSolicitudFarma.Add(nueva);
        //                this.reposolicituddata.InsertarSolicitudDataFarmaciasLite(nueva);
        //            }
        //        }
        //        Settings.CantFarmacias = dataFarmacias.Count;
        //        Settings.CurrentSolicitud = resultado.IdSolicitud;
        //    }
        //    Settings.CategoriaID = 0;
        //    await Navigation.PushAsync(new BuscadorProductoPage());
        //}

        //private void CotizaBtn_Clicked(object sender, EventArgs e)
        //{

        //}

        private void searchAddress_SearchButtonPressed(object sender, EventArgs e)
        {

        }

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //string textbtn = BuscarBtn.Text.ToLower();
            if (idubicacion == 0)
            {
                Settings.ErrorText = "Seleccione ubicacion";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
            if (enableds)
            {
                enableds = false;
                var view = sender as View;
                var categoriax = view?.BindingContext as Categoria;
                if (categoriax != null)
                {
                    Settings.ProductoMarcaID = 0;
                    Settings.CategoriaID = Convert.ToInt32(categoriax.CategoriaID);
                    await Navigation.PushAsync(new BuscadorProductoPage());
                }
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
                }



            }
        }

        async void comboFarmacias_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            //if (idubicacion == 0)
            //{
            //    Settings.ErrorText = "Seleccione ubicacion";
            //    await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
            //    return;
            //}
            //if (enableds)
            //{
            //    enableds = false;
            //    var seleccion = (UsuarioDireccion)e.Value;
            //    if (seleccion != null)
            //    {
            //        comboFarmacias.SelectedIndex = -1;
            //        await Navigation.PushAsync(new FarmaciasOnlinePage(lfarmaciasonline));
            //    }
            //}
        }

        async void searchBtn_SearchButtonPressed(object sender, EventArgs e)
        {
            if (idubicacion == 0)
            {
                Settings.ErrorText = "Seleccione ubicacion";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }

            string primertitulo = "";
            int indexspace = searchBtn.Text.IndexOf(" ");   //-1 no hay espacio > si hay 0 al inicio 
            if (string.IsNullOrEmpty(searchBtn.Text))
            {
                Settings.ErrorText = "El campo no puede estar vacio!";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
            if (searchBtn.Text.TrimEnd().Length < 4 && indexspace == -1)
            {
                Settings.ErrorText = "El campo debe contener mas caracteres o mas criterios de busqueda!";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
            Settings.ProductoBuscado = searchBtn.Text;
            await Navigation.PushAsync(new BuscadorProductoPage());
        }

        async void ListViewMarcas_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            if (idubicacion == 0)
            {
                Settings.ErrorText = "Seleccione ubicacion";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                ListViewMarcas.SelectedItem = null;
                return;
            }
            if (enableds)
            {
                enableds = false;
                var item = e.AddedItems;
                var marcasx = (ProductoMarca)item[0];
                if (marcasx != null)
                {
                    Settings.CategoriaID = 0;
                    Settings.ProductoMarcaID = Convert.ToInt32(marcasx.ProductoMarcaID);
                    await Navigation.PushAsync(new BuscadorProductoPage());
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
                //this.IsVisible = false;
                await Navigation.PushAsync(new FarmaciasOnlinePage(lfarmaciasonline));
            }
        }

        async void addAddressBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DireccionesPage());
        }
    }
}
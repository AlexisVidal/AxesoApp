using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Data;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using AxesoConsumer.Repositories;
using AxesoConsumer.ViewModels;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuscadorProductoPage : ContentPage
    {
        string currentsolicitud = "";
        ProductoLiteRepository repoproducto;
        SolicitudLiteRepository reposolicitud;
        SolicitudDataFarmaciasLiteRepository reposolicituddata;
        int cantresultados = 0;
        int cantfarmacias = 0;
        bool enabled = true;
        int categoriaID = 0;
        int productoMarcaID = 0;
        string productobuscado = Settings.ProductoBuscado;
        List<SolicitudLite> lsolicitudes = new List<SolicitudLite>();
        List<UsuarioDireccion> lfarmaciasonline = new List<UsuarioDireccion>();
        public BuscadorProductoPage()
        {
            try
            {
                InitializeComponent();
                PedidoBtn.IsVisible = false;
                this.reposolicitud = new SolicitudLiteRepository();
                this.reposolicituddata = new SolicitudDataFarmaciasLiteRepository();
                this.repoproducto = new ProductoLiteRepository();
                cantfarmacias = Settings.CantFarmacias;
                LoadSolicitudes();
                currentsolicitud = Settings.CurrentSolicitud;
                lbproductos.IsVisible = false;
                categoriaID = Settings.CategoriaID;
                if (categoriaID > 0)
                {
                    Settings.ProductoMarcaID = 0;
                    CargaProductosCategoria(categoriaID);
                }
                productoMarcaID = Settings.ProductoMarcaID;
                if (productoMarcaID > 0)
                {
                    Settings.CategoriaID = 0;
                    CargaProductosMarca(productoMarcaID);
                }
                if (!productobuscado.Equals(""))
                {
                    string primertitulo = "";
                    searchBtn.Text = productobuscado;
                    MuestraBusqueda(primertitulo);
                }
            }
            catch (Exception ex)
            {
            }
        }

        async void CargaProductosMarca(int productoMarcaID)
        {
            string primertitulo = "";
            try
            {
                ListViewCategory.ItemsSource = null;
                BasketItems.ItemsSource = null;
            }
            catch (Exception ex)
            {

            }

            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            this.IsEnabled = false;
            //var categoryx = await modelBL.GetCategorias();
            
            var productos = await modelBL.BuscaProductosByProductoMarcaID(productoMarcaID);

            lproductos = new List<Producto>();
            try
            {
                lproductos = (List<Producto>)productos.ToList();
            }
            catch (Exception ex)
            {
                lproductos = new List<Producto>();
            }
            if (productos == null)
            {
                cantresultados = 0;
            }
            else
            {
                cantresultados = productos.Count();
                var xcategorias = productos.GroupBy(elem => elem.Categoria.CategoriaID).Select(group => group.First());
                foreach (var item in xcategorias)
                {
                    Category x = new Category()
                    {
                        Image = "category.png",
                        Titulo = item.Categoria.Nombre,
                        Caption = item.Categoria.Abreviatura
                    };
                    MyCategorys.Add(x);
                }
                if (MyCategorys.Any())
                {

                    ListViewCategory.ItemsSource = MyCategorys;
                    primertitulo = MyCategorys.Select(x => x.Titulo.ToLower()).FirstOrDefault();
                }
                else
                {
                    ListViewCategory.ItemsSource = null;
                    lbproductos.IsVisible = false;
                }
            }
            Settings.CantResultado = cantresultados;
            string imagen = "";
            string mensaje = "";
            string mensaje2 = "";
            string mensaje3 = "";
            if (cantresultados <= 100)
            {
                imagen = "infoblue.png";
                mensaje = cantresultados + " productos encontrados";
            }
            else
            {
                imagen = "yellowexclamation.png";
                mensaje = "Se encontró un numero exesivo de productos coincidentes.";
                mensaje2 = "Solo se le mostrarán los primeros 100.";
                mensaje3 = "Para obtener mejores resultados, indique mas criterios de busqueda.";
            }
            await PopupNavigation.Instance.PushAsync(new CantProdPopUpPage(imagen, mensaje, mensaje2, mensaje3));
            if (productos.Any())
            {
                lbproductos.IsVisible = true;
                lbproductos.Text = "Resultados de categoria " + primertitulo;
                BasketItems.ItemsSource = lproductos.Where(x => x.Categoria.Nombre.ToLower().Equals(primertitulo)).Take(100).ToList();
            }
            else
            {
                lbproductos.IsVisible = false;
                BasketItems.ItemsSource = null;
            }
            this.IsEnabled = true;
            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        async void CargaProductosCategoria(int categoriaID)
        {
            string primertitulo = "";
            try
            {
                ListViewCategory.ItemsSource = null;
                BasketItems.ItemsSource = null;
            }
            catch (Exception ex)
            {

            }

            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            this.IsEnabled = false;
            var categoryx = await modelBL.GetCategorias();
            foreach (var item in categoryx)
            {
                if (item.CategoriaID == categoriaID)
                {
                    Category x = new Category()
                    {
                        Image = "category.png",
                        Titulo = item.Nombre,
                        Caption = item.Abreviatura
                    };
                    MyCategorys.Add(x);
                }
                
            }
            if (MyCategorys.Any())
            {

                ListViewCategory.ItemsSource = MyCategorys;
                primertitulo = MyCategorys.Select(x => x.Titulo.ToLower()).FirstOrDefault();
            }
            else
            {
                ListViewCategory.ItemsSource = null;
                lbproductos.IsVisible = false;
            }
            var productos = await modelBL.BuscaProductoCategoriaId(categoriaID);

            lproductos = new List<Producto>();
            try
            {
                lproductos = (List<Producto>)productos.ToList();
            }
            catch (Exception ex)
            {
                lproductos = new List<Producto>();
            }
            if (productos == null)
            {
                cantresultados = 0;
            }
            else
            {
                cantresultados = productos.Count();
            }
            
            Settings.CantResultado = cantresultados;
            string imagen = "";
            string mensaje = "";
            string mensaje2 = "";
            string mensaje3 = "";
            if (cantresultados <= 100)
            {
                imagen = "infoblue.png";
                mensaje = cantresultados + " productos encontrados";
            }
            else
            {
                imagen = "yellowexclamation.png";
                mensaje = "Se encontró un numero exesivo de productos coincidentes.";
                mensaje2 = "Solo se le mostrarán los primeros 100.";
                mensaje3 = "Para obtener mejores resultados, indique mas criterios de busqueda.";
            }
            await PopupNavigation.Instance.PushAsync(new CantProdPopUpPage(imagen, mensaje, mensaje2, mensaje3));







            if (productos.Any())
            {
                lbproductos.IsVisible = true;
                lbproductos.Text = "Resultados de categoria " + primertitulo;
                BasketItems.ItemsSource = lproductos.Where(x => x.Categoria.Nombre.ToLower().Equals(primertitulo)).Take(100).ToList();
            }
            else
            {
                lbproductos.IsVisible = false;
                BasketItems.ItemsSource = null;
            }



            this.IsEnabled = true;
            

            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
            
        }

        List<ProductoLite> lproducto = new List<ProductoLite>();
        SolicitudLite solicitude = new SolicitudLite();
        
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var ingresoactual = await Task.Run(() => CommonFunctions.GetSetIngreso(Settings.UserID));
            LoadSolicitudes();
            enabled = true;
            if (!Settings.FarmaciasOnlineList.Equals(""))
            {
                var listadofarmacias = JsonConvert.DeserializeObject<List<UsuarioDireccion>>(Settings.FarmaciasOnlineList);
                if (listadofarmacias != null)
                {
                    lfarmaciasonline = (List<UsuarioDireccion>)listadofarmacias;
                }
            }
        }
        
        private async void LoadSolicitudes()
        {
            try
            {
                solicitude = this.reposolicitud.BuscarSolicitudLiteByUser(Settings.UserEmail);
                
                if (solicitude != null)
                {
                    Settings.CurrentSolicitud = solicitude.IdSolicitud;
                    currentsolicitud = Settings.CurrentSolicitud;
                    lproducto = this.repoproducto.GetProductoLiteBySolicitud(currentsolicitud);
                    if (lproducto.Any())
                    {
                        PedidoBtn.IsVisible = false;
                    }
                    else
                    {
                        PedidoBtn.IsVisible = false;
                    }
                    
                }
                //lcantfarmacias.Text = string.Format("{0} Farmacias conectadas en {1}km a la redonda", cantfarmacias.ToString(), (Settings.Distancia / 1000).ToString());
                //lcantfarmacias.FontAttributes = FontAttributes.Italic | FontAttributes.Bold;
            }
            catch (Exception ex)
            {

            }
        }

        private string titulo;
        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                OnPropertyChanged(nameof(Titulo)); // Notify that there was a change on this property
            }
        }
        private ModelsBL modelBL = new ModelsBL();
        public List<Producto> lproductos = new List<Producto>();
        List<Producto> MyProducts = new List<Producto>();
        List<Category> MyCategorys = new List<Category>();
        async void searchBtn_SearchButtonPressed(object sender, EventArgs e)
        {
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
            try
            {
                ListViewCategory.ItemsSource = null;
                BasketItems.ItemsSource = null;
            }
            catch (Exception ex)
            {

            }
            MuestraBusqueda(primertitulo);
        }

        async void MuestraBusqueda(string primertitulo)
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.PushAsync(loadingPage);
            this.IsEnabled = false;
            var entidad = await modelBL.BuscaProducto(searchBtn.Text);
            if (entidad.Any())
            {
                var xcategorias = entidad.GroupBy(elem => elem.Categoria.CategoriaID).Select(group => group.First());
                MyCategorys = new List<Category>();
                lproductos = new List<Producto>();
                try
                {
                    lproductos = (List<Producto>)entidad.ToList();
                }
                catch (Exception ex)
                {

                }
                cantresultados = entidad.Count();
                Settings.CantResultado = cantresultados;
                string imagen = "";
                string mensaje = "";
                string mensaje2 = "";
                string mensaje3 = "";
                if (cantresultados <= 100)
                {
                    imagen = "infoblue.png";
                    mensaje = cantresultados + " productos encontrados";
                }
                else
                {
                    imagen = "yellowexclamation.png";
                    mensaje = "Se encontró un numero exesivo de productos coincidentes.";
                    mensaje2 = "Solo se le mostrarán los primeros 100.";
                    mensaje3 = "Para obtener mejores resultados, indique mas criterios de busqueda.";
                }
                await PopupNavigation.Instance.PushAsync(new CantProdPopUpPage(imagen, mensaje, mensaje2, mensaje3));

                foreach (var item in xcategorias)
                {
                    Category x = new Category()
                    {
                        Image = "category.png",
                        Titulo = item.Categoria.Nombre,
                        Caption = item.Categoria.Abreviatura
                    };
                    MyCategorys.Add(x);
                }
                if (MyCategorys.Any())
                {

                    ListViewCategory.ItemsSource = MyCategorys;
                    primertitulo = MyCategorys.Select(x => x.Titulo.ToLower()).FirstOrDefault();
                }
                else
                {
                    ListViewCategory.ItemsSource = null;
                    lbproductos.IsVisible = false;
                }
            }
            else
            {
                try
                {
                    ListViewCategory.ItemsSource = null;
                    BasketItems.ItemsSource = null;
                    lbproductos.IsVisible = false;
                    await PopupNavigation.Instance.PushAsync(new ResultadosPopUpPage());
                }
                catch (Exception ex)
                {

                }
            }
            await PopupNavigation.RemovePageAsync(loadingPage);
            this.IsEnabled = true;
            if (!primertitulo.Equals(""))
            {
                await MuestraProductosTitulo(primertitulo);
            }
        }

        async void ClickCategoryDetail(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;

            //this.BackgroundColor = Color.FromHex("#00acee");
            var titulox = args.Parameter.ToString().ToLower();
            MyProducts = new List<Producto>();
            await MuestraProductosTitulo(titulox);
        }

        async Task MuestraProductosTitulo(string titulox)
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            if (lproductos.Any())
            {
                lbproductos.IsVisible = true;
                lbproductos.Text = "Resultados de categoria " + titulox;
                BasketItems.ItemsSource = lproductos.Where(x => x.Categoria.Nombre.ToLower().Equals(titulox)).Take(100).ToList();
            }
            else
            {
                lbproductos.IsVisible = false;
                BasketItems.ItemsSource = null;
            }
            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        async void btnShowProducto_Clicked(object sender, EventArgs e)
        {

            var args = (Button)sender;
            var productoid = args.CommandParameter.ToString();
            if (Convert.ToInt32(productoid) <= 0)
            {
                return;
            }
            if (enabled)
            {
                enabled = false;
                Settings.ProductoId = Convert.ToInt32(productoid);
                await MuestraDetalle(Settings.ProductoId);
            }
        }

        async Task MuestraDetalle(int productoid)
        {
            if (!currentsolicitud.Equals(""))
            {

            }
            else
            {
                List<SolicitudDataFarmaciasLite> listSolicitudFarma = new List<SolicitudDataFarmaciasLite>();
                string solicitudID = Constants.GeneraPass();
                SolicitudLite solicito = new SolicitudLite
                {
                    IdSolicitud = solicitudID,
                    Usuario = Settings.UserEmail,
                    Fecha = DateTime.Now,
                    Activo = true,
                    Latitude = Settings.Latitude,
                    Longitud = Settings.Longitude,
                    Address = Settings.Address,
                    Distancia = Settings.Distancia,
                    Enviado = false
                };
                var resultado = this.reposolicitud.InsertarSolicitudLite(solicito);
                
                if (lfarmaciasonline.Any())
                {
                    foreach (var item in lfarmaciasonline)
                    {
                        SolicitudDataFarmaciasLite nueva = new SolicitudDataFarmaciasLite()
                        {
                            IdSolicitud = resultado.IdSolicitud,
                            UsuarioID = item.UsuarioID,
                            Activo = true
                        };
                        listSolicitudFarma.Add(nueva);
                        this.reposolicituddata.InsertarSolicitudDataFarmaciasLite(nueva);
                    }
                }
                Settings.CantFarmacias = lfarmaciasonline.Count;
                Settings.CurrentSolicitud = resultado.IdSolicitud;
                currentsolicitud = resultado.IdSolicitud;
            }
            ProductoLite currentproduct = new ProductoLite();


            var existe = this.repoproducto.BuscarProductoLiteIdFk(currentsolicitud, Convert.ToInt32(productoid));
            if (existe == null)
            {
                var entidadID = await modelBL.BuscaProductoId(Convert.ToInt32(productoid));
                if (entidadID.Any())
                {
                    currentproduct = new ProductoLite()
                    {
                        IdSolicitud = currentsolicitud,
                        ID = entidadID.FirstOrDefault().ID,
                        ProductoID = entidadID.FirstOrDefault().ProductoID,
                        TipoNegocioID = entidadID.FirstOrDefault().TipoNegocioID,
                        Nombre = entidadID.FirstOrDefault().Nombre,
                        Abreviatura = entidadID.FirstOrDefault().Abreviatura,
                        UnidadID_com = entidadID.FirstOrDefault().UnidadID_com,
                        UnidadNombre = entidadID.FirstOrDefault().UnidadMedida.Nombre,
                        UnidadID_gra = entidadID.FirstOrDefault().UnidadID_gra,
                        CategoriaID = entidadID.FirstOrDefault().Categoria.CategoriaID,
                        CategoriaNombre = entidadID.FirstOrDefault().Categoria.Nombre,
                        CategoriaAbreviatura = entidadID.FirstOrDefault().Categoria.Abreviatura,
                        Cantidad = 0,
                        Activo = false,
                        Imagen = entidadID.FirstOrDefault().Imagen,
                        PrecioRef_com = entidadID.FirstOrDefault().PrecioRef_com,
                        PrecioRef_gra = entidadID.FirstOrDefault().PrecioRef_gra,
                        RequiereReceta = entidadID.FirstOrDefault().RequiereReceta,
                        PrecioUnitario = entidadID.FirstOrDefault().PrecioRef_com,
                        PrecioTotal = 0,
                        UnidadId = entidadID.FirstOrDefault().UnidadID_com,
                        UnidadNombreSelect = entidadID.FirstOrDefault().UnidadMedida.Nombre
                    };
                    //var resultado = await App.Database.InsertProductoLite(currentproduct);
                    this.repoproducto.InsertarProductoLite(currentproduct);
                }
                else
                {
                    return;
                }
            }
            else
            {

            }

            await Navigation.PushAsync(new ProductoDetailPage());
        }

        private void BackButton(object sender, EventArgs e)
        {
            Settings.CategoriaID = 0;
            Settings.ProductoMarcaID = 0;
            Settings.ProductoBuscado = "";
            Navigation.PopAsync();
        }

        async void PedidoBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SolicitudPage());
        }

        async void ProductListRecognizer_Tapped(object sender, EventArgs e)
        {
            if (enabled)
            {
                enabled = false;
                var view = sender as View;
                var productx = view?.BindingContext as Producto;
                if (productx != null)
                {
                    Settings.ProductoId = Convert.ToInt32(productx.ProductoID);
                    await MuestraDetalle(productx.ProductoID);
                }
            }
        }
    }
}
using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using AxesoConsumer.Repositories;
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
    public partial class SolicitudPage : ContentPage
    {
        SolicitudLite solicitud = new SolicitudLite();
        List<ProductoLite> lproducto = new List<ProductoLite>();
        List<SolicitudDataFarmaciasLite> lsolicitudfarmacias = new List<SolicitudDataFarmaciasLite>();
        string currentSolicitud = "";
        ProductoLiteRepository repoproducto;
        SolicitudLiteRepository reposolicitud;
        SolicitudDataFarmaciasLiteRepository reposolicituddata;
        private ModelsBL modelBl = new ModelsBL();
        bool enableds = true;
        double rango = 0;

        public SolicitudPage()
        {
            InitializeComponent();
            rango = Settings.Distancia;
            this.reposolicitud = new SolicitudLiteRepository();
            this.reposolicituddata = new SolicitudDataFarmaciasLiteRepository();
            this.repoproducto = new ProductoLiteRepository();
            currentSolicitud = Settings.CurrentSolicitud;
            
            //LoadProdcutosSolicitud();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            enableds = true;
            try
            {
                ProductoItems.ItemsSource = null;
            }
            catch (Exception ex)
            {

            }
            rango = Settings.Distancia;
            LoadSolicitud();
        }
        async void LoadSolicitud()
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            try
            {
                
                //solicitud = await App.Database.GetSolicitud(currentSolicitud);
                solicitud = this.reposolicitud.BuscarSolicitudLite(currentSolicitud);
                if (solicitud != null)
                {
                    AddressLabel.Text = solicitud.Address;
                }
                //lproducto = await App.Database.GetProductoLiteByFk(currentSolicitud);
                lproducto = this.repoproducto.GetProductoLiteBySolicitud(currentSolicitud);
                if (lproducto.Any())
                {
                    ProductoItems.ItemsSource = lproducto;
                }
                else
                {
                    ProductoItems.ItemsSource = null;
                }

                lsolicitudfarmacias = this.reposolicituddata.BuscarSolicitudDataFarmaciasLitesByFk(currentSolicitud);
                lcantfarmaciassolicitud.Text = lsolicitudfarmacias.Count.ToString() + " Farmacias conectadas en " + (rango / 1000).ToString() + "km a la redonda";

                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
            }
        }

        async void LoadProdcutosSolicitud()
        {

            try
            {
                var loadingPage = new LoadingPopupPage();
                await PopupNavigation.Instance.PushAsync(loadingPage);
                //lproducto = await App.Database.GetProductoLiteByFk(currentSolicitud);
                lproducto = this.repoproducto.GetProductoLiteBySolicitud(currentSolicitud);
                if (lproducto.Any())
                {
                    ProductoItems.ItemsSource = lproducto;
                }
                else
                {
                    ProductoItems.ItemsSource = null;
                }
                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
            }
            catch (Exception ex)
            {

            }
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ProductoLite currentproduct = new ProductoLite();
            if (enableds)
            {
                enableds = false;
                var view = sender as View;
                var productx = view?.BindingContext as ProductoLite;
                if (productx != null)
                {
                    Settings.ProductoId = Convert.ToInt32(productx.ProductoID);
                    var entidadID = this.repoproducto.BuscarProductoLiteIdFk(currentSolicitud, Convert.ToInt32(productx.ProductoID));
                    if (entidadID != null)
                    {
                        currentproduct = new ProductoLite()
                        {
                            IdSolicitud = currentSolicitud,
                            ID = entidadID.ID,
                            ProductoID = entidadID.ProductoID,
                            TipoNegocioID = entidadID.TipoNegocioID,
                            Nombre = entidadID.Nombre,
                            Abreviatura = entidadID.Abreviatura,
                            UnidadID_com = entidadID.UnidadID_com,
                            UnidadNombre = entidadID.UnidadNombre,
                            UnidadID_gra = entidadID.UnidadID_gra,
                            CategoriaID = entidadID.CategoriaID,
                            CategoriaNombre = entidadID.CategoriaNombre,
                            CategoriaAbreviatura = entidadID.CategoriaAbreviatura,
                            Cantidad = entidadID.Cantidad,
                            Activo = entidadID.Activo,
                            Imagen = entidadID.Imagen
                        };
                        //var resultado = await App.Database.InsertProductoLite(currentproduct);
                        //this.repoproducto.InsertarProductoLite(currentproduct);
                    }
                    else
                    {
                        return;
                    }
                    await Navigation.PushAsync(new ProductoDetailPage());
                }
            }
        }

        async void btnModificaProduct_Clicked(object sender, EventArgs e)
        {
            ProductoLite currentproduct = new ProductoLite();
            var args = (Button)sender;
            var productoid = args.CommandParameter.ToString();
            if (Convert.ToInt32(productoid) <= 0)
            {
                return;
            }
            Settings.ProductoId = Convert.ToInt32(productoid);
            //var entidadID = await App.Database.GetProductoLiteByFkId(currentSolicitud, Convert.ToInt32(productoid));
            var entidadID = this.repoproducto.BuscarProductoLiteIdFk(currentSolicitud, Convert.ToInt32(productoid));
            if (entidadID != null)
            {
                currentproduct = new ProductoLite()
                {
                    IdSolicitud = currentSolicitud,
                    ID = entidadID.ID,
                    ProductoID = entidadID.ProductoID,
                    TipoNegocioID = entidadID.TipoNegocioID,
                    Nombre = entidadID.Nombre,
                    Abreviatura = entidadID.Abreviatura,
                    UnidadID_com = entidadID.UnidadID_com,
                    UnidadNombre = entidadID.UnidadNombre,
                    UnidadID_gra = entidadID.UnidadID_gra,
                    CategoriaID = entidadID.CategoriaID,
                    CategoriaNombre = entidadID.CategoriaNombre,
                    CategoriaAbreviatura = entidadID.CategoriaAbreviatura,
                    Cantidad = entidadID.Cantidad,
                    Activo = entidadID.Activo,
                    Imagen = entidadID.Imagen
                };
                //var resultado = await App.Database.InsertProductoLite(currentproduct);
                //this.repoproducto.InsertarProductoLite(currentproduct);
            }
            else
            {
                return;
            }
            await Navigation.PushAsync(new ProductoDetailPage());
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
                    FechaEnviado = "Enviado el "+ DateTime.Now.ToString("dd/MM/yyyy") + " a las " + DateTime.Now.ToString("H:mm"),
                    Cotizado =false
                };
                var registro = await modelBl.AddSolicitud(entidadinsert);
                if (registro != null)
                {
                    int cantidaditems = lproducto.Count();
                    int contador = 0;
                    int contador2 = 0;
                    int idinsertado = 0;
                    var insertado = await modelBl.GetAllSolicitudesByUsuario(Settings.UserID);
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
                            RequiereReceta = item.RequiereReceta
                        };
                        var registrodetail = await modelBl.AddSolicitudProducto(detalleinsert);
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
                        var registrodetailfarmas = await modelBl.AddSolicitudDataFarmacias(detallefarmas);
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
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
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmaCotizacionPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        ModelsBL modelbl = new ModelsBL();
        int usuarioID = Settings.UserID;
        bool enabled = true;
        List<CotizacionProducto> lproductosonline = new List<CotizacionProducto>();
        Cotizacion cotizacion = new Cotizacion();
        Solicitud solicitud = new Solicitud();

        CultureInfo ci = new CultureInfo("es-PE");
        List<FormaPago> lformapago = new List<FormaPago>();
        decimal pagarcon = 0;
        decimal porpagar = 0;
        string formapagar = "";
        public ConfirmaCotizacionPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            lformapago = new List<FormaPago>()
            {
                new FormaPago(){FormaPagar="Efectivo"},
                new FormaPago(){FormaPagar="Tarjeta"}
            };
            var ingresoactual = await Task.Run(() => CommonFunctions.GetSetIngreso(Settings.UserID));
            enabled = true;

            if (!Settings.CotizacionJson.Equals(""))
            {
                var cotixacion = JsonConvert.DeserializeObject<Cotizacion>(Settings.CotizacionJson);
                if (cotixacion != null)
                {
                    cotizacion = (Cotizacion)cotixacion;
                    var solicitudx = await modelbl.GetAllSolicitudesByUsuarioID(usuarioID, cotizacion.SolicitudID);
                    if (solicitudx.Any())
                    {
                        solicitud = (Solicitud)solicitudx.FirstOrDefault();
                    }
                }

            }

            if (!Settings.CotizacionProductoJson.Equals(""))
            {
                var listcotiproducto = JsonConvert.DeserializeObject<List<CotizacionProducto>>(Settings.CotizacionProductoJson);
                if (listcotiproducto != null)
                {
                    lproductosonline = (List<CotizacionProducto>)listcotiproducto;
                    if (lproductosonline != null && lproductosonline.Any())
                    {
                        FarmaSolicDireccion.Text = lproductosonline.FirstOrDefault().Cotizacion.Solicitud.Direccion;
                        ltproductos.Text = string.Format(ci, "{0:0.00}", lproductosonline.Sum(x => x.PrecioTotal));
                        porpagar = lproductosonline.Sum(x => x.PrecioTotal);
                    }
                    PickerForma.DisplayMemberPath = "FormaPagar";
                    PickerForma.SelectedValuePath = "FormaPagar";
                    PickerForma.DataSource = lformapago;
                }
            }
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void PickerForma_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            
            formapagar = "";
            
            var seleccion = (FormaPago)e.Value;
            if (seleccion != null)
            {
                int position = PickerForma.SelectedIndex;
                if (position > -1)
                {
                    formapagar = seleccion.FormaPagar;
                }
                else
                {
                    formapagar = "";
                }
            }

        }

        async void btnAceptarCotizacion_Clicked(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            try
            {
                if (lpagaproductos.Text == "")
                {
                    Settings.ErrorText = "Ingrese una cantidad de pago!";
                    await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                    return;
                }
                else if (formapagar == "")
                {
                    Settings.ErrorText = "Seleccione forma de pago!";
                    await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                    return;
                }
                else
                {
                    pagarcon = Convert.ToDecimal(lpagaproductos.Text);
                    if (pagarcon < porpagar)
                    {
                        Settings.ErrorText = "El monto a pagar debe ser mayor o igual al monto total!";
                        await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                        return;
                    }
                    else
                    {
                        
                        await PopupNavigation.Instance.PushAsync(loadingPage);

                        var xpedidos = await modelbl.GetAllPedidos();
                        string numerox = DateTime.Now.Day.ToString("0#") + DateTime.Now.Month.ToString("0#") + DateTime.Now.ToString("yy") + "1".ToString().PadLeft(6,'0');
                        if (xpedidos != null)
                        {
                            if (xpedidos.Any())
                            {
                                var lpedidoshoy = xpedidos.Where(x => x.Fecha.Date == DateTime.Now.Date).ToList();
                                if (lpedidoshoy.Any())
                                {
                                    int siguiente = lpedidoshoy.Count + 1;
                                    numerox = DateTime.Now.Day.ToString("0#") + DateTime.Now.Month.ToString("0#") + DateTime.Now.ToString("yy") + siguiente.ToString().PadLeft(6, '0');
                                }
                            }
                        }
                        PedidoInput pedidoinsert = new PedidoInput()
                        {
                            Numero = numerox,
                            CotizacionID = cotizacion.CotizacionID,
                            UsuarioID = cotizacion.UsuarioID,
                            SolicitudID = cotizacion.SolicitudID,
                            Activo = true,
                            Fecha = DateTime.Now,
                            FechaGenerado = DateTime.Now.ToString(),
                            Nombre = cotizacion.Nombre,
                            Direccion = cotizacion.Direccion,
                            Latitud = cotizacion.Latitud,
                            Longitud = cotizacion.Longitud,
                            Estado = "1",
                            NombreEntrega = solicitud.Usuarios.Descripcion,
                            DireccionEntrega = solicitud.Direccion,
                            LatitudEntrega = solicitud.Latitud,
                            LongitudEntrega = solicitud.Longitud,
                            TotalPagar = porpagar,
                            TipoPagar = formapagar,
                            MontoPagar = pagarcon,
                            UsuarioClienteID = usuarioID
                        };
                        var registro = await modelbl.AddPedido(pedidoinsert);
                        if (registro != null)
                        {
                            var insertado = await modelbl.GetAllPedidosBySolicitud(cotizacion.SolicitudID);
                            var need = (Pedido)insertado.Where(x => x.CotizacionID == cotizacion.CotizacionID && x.Activo == true).FirstOrDefault();
                            PedidoLineaTiempo pedibd = new PedidoLineaTiempo();
                            var pedidolineabd = await modelbl.GetAllPedidoLineaTiempoByPedido(need.PedidoID);
                            if (pedidolineabd.Any())
                            {
                                var existe = pedidolineabd.Where(x => x.EstadoPedidoID.Equals("1")).FirstOrDefault();
                                if (existe != null)
                                {
                                    pedibd = (PedidoLineaTiempo)existe;
                                    #region firebase
                                    PedidoLineaTiempo pedilinea = new PedidoLineaTiempo()
                                    {
                                        PedidoLineaTiempoID = pedibd.PedidoLineaTiempoID,
                                        PedidoID = pedibd.PedidoID,
                                        EstadoPedidoID = pedibd.EstadoPedidoID,
                                        Descripcion = pedibd.Descripcion,
                                        Fecha = pedibd.Fecha,
                                        FechaTexto = pedibd.FechaTexto,
                                        Numero = pedibd.Numero,
                                        UsuarioClienteID = pedibd.UsuarioClienteID
                                    };
                                    await firebaseHelper.AddPedidoLineaTiempo(pedilinea);
                                    #endregion
                                }
                            }
                            

                            foreach (var item in lproductosonline)
                            {
                                PedidoProductoInput pediprod = new PedidoProductoInput()
                                {
                                    PedidoID = need.PedidoID,
                                    CotizacionProductoID = item.CotizacionProductoID,
                                    CotizacionID = item.CotizacionID,
                                    SolicitudProductoID = item.SolicitudProductoID,
                                    ProductoID = item.SolicitudProducto.ProductoID,
                                    TipoNegocioID = item.SolicitudProducto.TipoNegocioID,
                                    Nombre = item.SolicitudProducto.Nombre,
                                    Abreviatura = item.SolicitudProducto.Abreviatura,
                                    UnidadId = item.SolicitudProducto.UnidadID,
                                    UnidadNombre = item.SolicitudProducto.UnidadNombre,
                                    CategoriaID = item.SolicitudProducto.CategoriaID,
                                    CategoriaNombre = item.SolicitudProducto.CategoriaNombre,
                                    CategoriaAbreviatura = item.SolicitudProducto.CategoriaAbreviatura,
                                    Cantidad = item.Cantidad,
                                    Activo = true,
                                    Imagen = item.SolicitudProducto.Imagen,
                                    PrecioUnitario = item.PrecioUnitario,
                                    PrecioTotal = item.PrecioTotal,
                                    RequiereReceta = item.SolicitudProducto.RequiereReceta
                                };
                                var detallespedi = await modelbl.AddPedidoProducto(pediprod);
                            }
                            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                            Settings.SuccessText = "Registro exito";
                            await PopupNavigation.Instance.PushAsync(new SuccessPopUpPage());
                            btnAceptarCotizacion.IsEnabled = false;
                            Navigation.PopAsync();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                return;
            }

        }
    }
}
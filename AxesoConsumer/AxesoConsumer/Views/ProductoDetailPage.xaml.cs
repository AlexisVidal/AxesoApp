using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Data;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using AxesoConsumer.Repositories;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
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
    public partial class ProductoDetailPage : ContentPage
    {
        ProductoLite currentproduct = new ProductoLite();
        string currentSolicitudId = "";
        int currentProductId = 0;
        int currencant = 0;
        int unidadid = 0;
        string unidadnomb = "";
        ProductoLiteRepository repoproducto;
        SolicitudLiteRepository reposolicitud;
        List<UnidadMedida> unidadMedidas = new List<UnidadMedida>();
        List<UnidadMedidaTemp> unidadMedidaslist = new List<UnidadMedidaTemp>();
        private ModelsBL modelBL = new ModelsBL();
        List<ProductoLite> lproducto = new List<ProductoLite>();
        CultureInfo ci = new CultureInfo("es-PE");
        public ProductoDetailPage()
        {
            try
            {
                InitializeComponent();
                this.reposolicitud = new SolicitudLiteRepository();
                this.repoproducto = new ProductoLiteRepository();
                currentSolicitudId = Settings.CurrentSolicitud;
                currentProductId = Settings.ProductoId;
                PedidoBtnP.IsVisible = false;
                LoadProducto();
            }
            catch (Exception ex)
            {

            }
        }

        async void LoadCategorias()
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            try
            {
                var entidad = await modelBL.GetUnidadMedidas();
                if (entidad.Any())
                {
                    unidadMedidas = (List<UnidadMedida>)entidad.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        async void LoadProducto()
        {
            try
            {
                var loadingPage = new LoadingPopupPage();
                await PopupNavigation.Instance.PushAsync(loadingPage);
                try
                {

                    lproducto = this.repoproducto.GetProductoLiteBySolicitud(currentSolicitudId);
                    if (lproducto.Any())
                    {
                        PedidoBtnP.IsVisible = true;
                    }
                    else
                    {
                        PedidoBtnP.IsVisible = false;
                    }
                }
                catch (Exception ex)
                {

                }
                try
                {
                    var entidad = await modelBL.GetUnidadMedidas();
                    if (entidad.Any())
                    {
                        unidadMedidas = (List<UnidadMedida>)entidad.ToList();
                    }
                }
                catch (Exception ex)
                {

                }
                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                //currentproduct = await App.Database.GetProductoLiteByFkId(currentSolicitudId, currentProductId);
                currentproduct = this.repoproducto.BuscarProductoLiteIdFk(currentSolicitudId, currentProductId);
                if (currentproduct.ID > 0)
                {
                    unidadMedidaslist.Add(new UnidadMedidaTemp()
                    {
                        UnidadID = currentproduct.UnidadID_com,
                        Nombre = currentproduct.UnidadNombre
                    });
                    if (currentproduct.UnidadID_gra != null && unidadMedidas.Any())
                    {
                        var unidfound = unidadMedidas.Where(x => x.UnidadID == currentproduct.UnidadID_gra).FirstOrDefault();
                        if (unidfound != null)
                        {
                            unidadMedidaslist.Add(new UnidadMedidaTemp()
                            {
                                UnidadID = unidfound.UnidadID,
                                Nombre = unidfound.Nombre
                            });
                        }

                    }
                    //PickerUM.ItemsSource = unidadMedidaslist;

                    PickerUM.DisplayMemberPath = "Nombre";
                    PickerUM.SelectedValuePath = "UnidadID";
                    PickerUM.DataSource = unidadMedidaslist;

                    int positions = 0;
                    if (currentproduct.UnidadId == 0)
                    {
                        positions = unidadMedidaslist.FindIndex(X => X.UnidadID == currentproduct.UnidadID_com);
                    }
                    else
                    {
                        positions = unidadMedidaslist.FindIndex(X => X.UnidadID == currentproduct.UnidadId);
                    }

                    PickerUM.SelectedIndex = positions;
                    ImagenProduct.Source = currentproduct.Imagen;
                    ProductName.Text = currentproduct.Nombre;
                    ProductDescripcion.Text = currentproduct.Nombre
                        + ". Abreviatura: " + currentproduct.Abreviatura
                        + ". Categoria: " + currentproduct.CategoriaNombre;
                    if (currentproduct.RequiereReceta)
                    {
                        lblReceta.IsVisible = true;
                    }
                    if (positions == 0)
                    {
                        PrecioProduct.Text = string.Format(ci, "{0:0.00}", currentproduct.PrecioRef_com);
                    }
                    else
                    {
                        PrecioProduct.Text = string.Format(ci, "{0:0.00}", currentproduct.PrecioRef_gra);
                    }
                    //PresentProduct.Text = PresentProduct.Text + " " + currentproduct.UnidadNombre;
                    //AbrevProduct.Text = AbrevProduct.Text + " " + currentproduct.Abreviatura;
                    stepper.Value = currentproduct.Cantidad;
                    if (currentproduct.Cantidad == 0)
                    {
                        //btnDeleteProducto.IsVisible = false;
                        //btnInsertProducto.Text = "Agregar";
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        async void BackButton(object sender, EventArgs e)
        {
            int cantix = Convert.ToInt32(stepper.Value);
            if (cantix == 0)
            {
                var retorno = await DeleteLocalProduct(currentSolicitudId, currentProductId);
                if (retorno == 1)
                {
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        async void btnInsertProducto_Clicked(object sender, EventArgs e)
        {
            currentproduct = this.repoproducto.BuscarProductoLiteIdFk(currentSolicitudId, currentProductId);
            int cantix = Convert.ToInt32(stepper.Value);
            if (cantix == 0)
            {
                Settings.ErrorText = "Seleccione una cantidad superior a 0";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
            if (unidadid == 0)
            {
                Settings.ErrorText = "Seleccione una presentación";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
            try
            {

                this.repoproducto.EliminarProductoLite(currentproduct.ProductoLiteID);
                ProductoLite newproduc = new ProductoLite()
                {
                    IdSolicitud = currentproduct.IdSolicitud,
                    ID = currentproduct.ID,
                    ProductoID = currentproduct.ProductoID,
                    TipoNegocioID = currentproduct.TipoNegocioID,
                    Nombre = currentproduct.Nombre,
                    Abreviatura = currentproduct.Abreviatura,
                    UnidadID_com = currentproduct.UnidadID_com,
                    UnidadNombre = currentproduct.UnidadNombre,
                    UnidadID_gra = currentproduct.UnidadID_gra,
                    CategoriaID = currentproduct.CategoriaID,
                    CategoriaNombre = currentproduct.Nombre,
                    CategoriaAbreviatura = currentproduct.Abreviatura,
                    Cantidad = cantix,
                    Activo = true,
                    Imagen = currentproduct.Imagen,
                    UnidadId = unidadid,
                    UnidadNombreSelect = unidadnomb
                };
                this.repoproducto.InsertarProductoLite(newproduc);
                Settings.SuccessText = "Registro exito";
                await PopupNavigation.Instance.PushAsync(new SuccessPopUpPage());
                //btnDeleteProducto.IsVisible = true;
                //btnInsertProducto.Text = "Actualizar";
                return;
            }
            catch (Exception ex)
            {

            }
        }

        async void btnDeleteProducto_Clicked(object sender, EventArgs e)
        {
            var retorno = await DeleteLocalProduct(currentSolicitudId, currentProductId);
            if (retorno == 1)
            {
                Settings.SuccessText = "Eliminacion exitosa!";
                await PopupNavigation.Instance.PushAsync(new SuccessPopUpPage());
                await Navigation.PopAsync();
            }
        }

        async Task<int> DeleteLocalProduct(string currentSolicitudIdx, int currentProductIdx)
        {
            try
            {
                currentproduct = this.repoproducto.BuscarProductoLiteIdFk(currentSolicitudIdx, currentProductIdx);
                ProductoLite newproduc = new ProductoLite()
                {
                    ProductoLiteID = currentproduct.ProductoLiteID,
                    IdSolicitud = currentproduct.IdSolicitud,
                    ID = currentproduct.ID,
                    ProductoID = currentproduct.ProductoID,
                    TipoNegocioID = currentproduct.TipoNegocioID,
                    Nombre = currentproduct.Nombre,
                    Abreviatura = currentproduct.Abreviatura,
                    UnidadID_com = currentproduct.UnidadID_com,
                    UnidadNombre = currentproduct.UnidadNombre,
                    UnidadID_gra = currentproduct.UnidadID_gra,
                    CategoriaID = currentproduct.CategoriaID,
                    CategoriaNombre = currentproduct.Nombre,
                    CategoriaAbreviatura = currentproduct.Abreviatura,
                    Cantidad = currentproduct.Cantidad,
                    Activo = currentproduct.Activo,
                    Imagen = currentproduct.Imagen,
                    UnidadId = currentproduct.UnidadId,
                    UnidadNombreSelect = currentproduct.UnidadNombreSelect
                };
                this.repoproducto.EliminarProductoLite(newproduc.ProductoLiteID);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private void PickerUM_SelectedIndexChanged(object sender, EventArgs e)
        {
            int position = PickerUM.SelectedIndex;
            unidadid = 0;
            unidadnomb = "";
            if (position > -1)
            {
                unidadnomb = unidadMedidaslist[position].Nombre;
                unidadid = Convert.ToInt32(unidadMedidaslist[position].UnidadID);
            }
            if (position == 0)
            {
                PrecioProduct.Text = string.Format(ci, "{0:0.00}", currentproduct.PrecioRef_com);
            }
            else
            {
                PrecioProduct.Text = string.Format(ci, "{0:0.00}", currentproduct.PrecioRef_gra);
            }
        }

        async void PedidoBtnP_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SolicitudPage());
        }

        private void PickerUM_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            unidadid = 0;
            unidadnomb = "";
            var seleccion = (UnidadMedidaTemp)e.Value;
            if (seleccion != null)
            {
                int position = PickerUM.SelectedIndex;
                if (position == 0)
                {
                    PrecioProduct.Text = string.Format(ci, "{0:0.00}", currentproduct.PrecioRef_com);
                }
                else
                {
                    PrecioProduct.Text = string.Format(ci, "{0:0.00}", currentproduct.PrecioRef_gra);
                }
                unidadnomb = seleccion.Nombre;
                unidadid = seleccion.UnidadID;
            }
        }
        ToastLength toastLength = ToastLength.Short;
        async void stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                if (e.NewValue > 0)
                {
                    try
                    {
                        decimal dPrecioUnitario = Convert.ToDecimal(Convert.ToDouble(PrecioProduct.Text));
                        decimal dPrecioTotal = Convert.ToDecimal(Convert.ToInt32(e.NewValue) * Convert.ToDouble(PrecioProduct.Text));
                        var currentproduct2 = this.repoproducto.BuscarProductoLiteIdFk(currentSolicitudId, currentProductId);
                        if (currentproduct2 != null)
                        {
                            this.repoproducto.ModificarProductoLite(currentproduct2.ProductoLiteID, Convert.ToInt32(e.NewValue), true, dPrecioUnitario, dPrecioTotal);
                        }
                        else
                        {
                            ProductoLite newproduc = new ProductoLite()
                            {
                                IdSolicitud = currentproduct.IdSolicitud,
                                ID = currentproduct.ID,
                                ProductoID = currentproduct.ProductoID,
                                TipoNegocioID = currentproduct.TipoNegocioID,
                                Nombre = currentproduct.Nombre,
                                Abreviatura = currentproduct.Abreviatura,
                                UnidadID_com = currentproduct.UnidadID_com,
                                UnidadNombre = currentproduct.UnidadNombre,
                                UnidadID_gra = currentproduct.UnidadID_gra,
                                CategoriaID = currentproduct.CategoriaID,
                                CategoriaNombre = currentproduct.Nombre,
                                CategoriaAbreviatura = currentproduct.Abreviatura,
                                Cantidad = Convert.ToInt32(e.NewValue),
                                Activo = true,
                                Imagen = currentproduct.Imagen,
                                UnidadId = unidadid,
                                UnidadNombreSelect = unidadnomb,
                                ProductoMarcaID = currentproduct.ProductoMarcaID,
                                PrecioUnitario = dPrecioUnitario,
                                PrecioTotal = dPrecioTotal
                            };

                            this.repoproducto.InsertarProductoLite(newproduc);
                        }
                        if (e.OldValue == 0 && e.NewValue == 1)
                        {
                            CrossToastPopUp.Current.ShowToastSuccess("El producto se agregó al carrito de compras", toastLength);
                        }
                        return;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (e.NewValue == 0)
                {
                    var retorno = await DeleteLocalProduct(currentSolicitudId, currentProductId);
                    if (retorno == 1)
                    {
                        CrossToastPopUp.Current.ShowToastWarning("El producto se eliminó del carrito de compras", toastLength);
                        return;
                    }
                }
            }
        }
    }
}
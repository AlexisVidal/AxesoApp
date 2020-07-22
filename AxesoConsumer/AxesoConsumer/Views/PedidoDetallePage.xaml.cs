using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoDetallePage : ContentPage
    {
        int pedidoID = Settings.PedidoID;
        ModelsBL modelbl = new ModelsBL();
        Pedido pedido = new Pedido();
        List<PedidoProducto> pedidodetalles = new List<PedidoProducto>();
        public PedidoDetallePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CultureInfo myCurrency = new CultureInfo("es-PE");
            CultureInfo.DefaultThreadCurrentCulture = myCurrency;
            LoadData();
        }

        async void LoadData()
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            try
            {
                

                pedido = new Pedido();
                var xpedido = await modelbl.GetAllPedidosByPedido(pedidoID);
                if (xpedido != null)
                {
                    FarmaLabel.Text = xpedido.FirstOrDefault().Nombre;
                    FarmaDireccionLabel.Text = xpedido.FirstOrDefault().Direccion;
                    FarmaSolicDireccion.Text = xpedido.FirstOrDefault().DireccionEntrega;
                    ltproductos.Text = xpedido.FirstOrDefault().TotalPagar.ToString("0.#0");
                    lstproductos.Text = xpedido.FirstOrDefault().TotalPagar.ToString("0.#0");
                    ltformapago.Text = xpedido.FirstOrDefault().TipoPagar.ToString();
                    ltpagocuanto.Text = xpedido.FirstOrDefault().MontoPagar.ToString("0.#0");
                    FarmaSolicDireccion.Text = xpedido.FirstOrDefault().DireccionEntrega;
                    pedidodetalles = new List<PedidoProducto>();
                    var xdetalles = await modelbl.GetAllPedidoProductoByPedido(pedidoID);
                    if (xdetalles.Any())
                    {
                        pedidodetalles = (List<PedidoProducto>)xdetalles.ToList();
                        PedidoItems.ItemsSource = pedidodetalles;
                    }
                    else
                    {
                        PedidoItems.ItemsSource = null;
                    }
                }
                
            }
            catch (Exception ex)
            {
                PedidoItems.ItemsSource = null;
            }
            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
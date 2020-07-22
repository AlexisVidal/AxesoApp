using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstadoPedidoPage : ContentPage
    {
        int pedidoID = Settings.PedidoID;
        string spedido = "Pedido ";
        ModelsBL model = new ModelsBL();
        int contador = 1;
        int contadortotal = 0;
        List<PedidoLineaTiempo> presult = new List<PedidoLineaTiempo>();
        List<PedidoLineaTiempoList> plresult = new List<PedidoLineaTiempoList>();
        public EstadoPedidoPage()
        {
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargaEstados(pedidoID);
        }
        private async void CargaEstados(int pedidoID)
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);

            plresult = new List<PedidoLineaTiempoList>();
            var result = await model.GetAllPedidoLineaTiempoByPedido(pedidoID);
            if (result != null)
            {
                presult = result.ToList();
                contadortotal = presult.Count;
                string statu = "Recibido";
                spedido = "";
                spedido = "Pedido " + presult.Select(x => x.Numero).FirstOrDefault();
                foreach (var item in presult.OrderBy(x => x.EstadoPedidoID).ToList())
                {
                    switch (item.EstadoPedidoID)
                    {
                        case "1":
                            statu = "Recibido";
                            break;
                        case "2":
                            statu = "Preparando";
                            break;
                        case "3":
                            statu = "Enviado";
                            break;
                        case "4":
                            statu = "Entregado";
                            break;
                        case "5":
                            statu = "Rechazado";
                            break;
                    }
                    bool last = false;
                    if (contador == contadortotal)
                    {
                        last = true;
                    }

                    PedidoLineaTiempoList newpedidolinea = new PedidoLineaTiempoList()
                    {
                        PedidoLineaTiempoID = item.PedidoLineaTiempoID,
                        PedidoID = item.PedidoID,
                        EstadoPedidoID = item.EstadoPedidoID,
                        Descripcion = item.Descripcion.Substring(item.Descripcion.IndexOf(" ")),
                        Fecha = item.Fecha,
                        FechaTexto = item.FechaTexto.Substring(0, 5),
                        IsLast = last,
                        Estado = statu,
                        Numero = item.Numero
                    };
                    plresult.Add(newpedidolinea);
                    contador++;
                }
                BindingContext = plresult;
                ltitulo.Text = spedido;
            }
            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void timelineListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private async void btnVerPedido_Clicked(object sender, EventArgs e)
        {
            if (pedidoID > 0)
            {
                await Navigation.PushAsync(new PedidoDetallePage());
            }
        }
    }
}
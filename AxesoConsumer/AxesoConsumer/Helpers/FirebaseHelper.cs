using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Axeso_BE;
using Firebase.Database;
using Newtonsoft.Json;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;

namespace AxesoConsumer.Helpers
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://axesoapp-a802c.firebaseio.com/");

        #region Pedido
        public async Task<List<Pedido>> GetAllPedidos()
        {

            return (await firebase
              .Child("Pedidos")
              .OnceAsync<Pedido>()).Select(item => new Pedido
              {
                  PedidoID = item.Object.PedidoID,
                  Numero = item.Object.Numero,
                  CotizacionID = item.Object.CotizacionID,
                  UsuarioID = item.Object.UsuarioID,
                  SolicitudID = item.Object.SolicitudID,
                  Activo = item.Object.Activo,
                  Fecha = item.Object.Fecha,
                  FechaGenerado = item.Object.FechaGenerado,
                  Nombre = item.Object.Nombre,
                  Direccion = item.Object.Direccion,
                  Latitud = item.Object.Latitud,
                  Longitud = item.Object.Longitud,
                  Estado = item.Object.Estado,
                  NombreEntrega = item.Object.NombreEntrega,
                  DireccionEntrega = item.Object.DireccionEntrega,
                  LatitudEntrega = item.Object.LatitudEntrega,
                  LongitudEntrega = item.Object.LongitudEntrega,
                  TotalPagar = item.Object.TotalPagar,
                  TipoPagar = item.Object.TipoPagar,
                  MontoPagar = item.Object.MontoPagar,
                  UsuarioClienteID = item.Object.UsuarioClienteID
              }).ToList();
        }

        public async Task AddPedido(Pedido pedido)
        {

            await firebase
              .Child("Pedidos")
              .PostAsync(new Pedido()
              {
                  PedidoID = pedido.PedidoID,
                  Numero = pedido.Numero,
                  CotizacionID = pedido.CotizacionID,
                  UsuarioID = pedido.UsuarioID,
                  SolicitudID = pedido.SolicitudID,
                  Activo = pedido.Activo,
                  Fecha = pedido.Fecha,
                  FechaGenerado = pedido.FechaGenerado,
                  Nombre = pedido.Nombre,
                  Direccion = pedido.Direccion,
                  Latitud = pedido.Latitud,
                  Longitud = pedido.Longitud,
                  Estado = pedido.Estado,
                  NombreEntrega = pedido.NombreEntrega,
                  DireccionEntrega = pedido.DireccionEntrega,
                  LatitudEntrega = pedido.LatitudEntrega,
                  LongitudEntrega = pedido.LongitudEntrega,
                  TotalPagar = pedido.TotalPagar,
                  TipoPagar = pedido.TipoPagar,
                  MontoPagar = pedido.MontoPagar,
                  UsuarioClienteID = pedido.UsuarioClienteID
              });
        }

        public async Task<Pedido> GetPedido(int pedidoID)
        {
            var allPedidos = await GetAllPedidos();
            await firebase
              .Child("Pedidos")
              .OnceAsync<Pedido>();
            return allPedidos.Where(a => a.PedidoID == pedidoID).FirstOrDefault();
        }

        public async Task UpdatePedido(int pedidoID, string estado)
        {
            var toUpdatePedido = (await firebase
              .Child("Pedidos")
              .OnceAsync<Pedido>()).Where(a => a.Object.PedidoID == pedidoID).FirstOrDefault();

            await firebase
              .Child("Pedidos")
              .Child(toUpdatePedido.Key)
              .PutAsync(new Pedido() { PedidoID = pedidoID, Estado = estado });
        }

        public async Task DeletePedido(int pedidoID)
        {
            var toDeletePedido = (await firebase
              .Child("Pedidos")
              .OnceAsync<Pedido>()).Where(a => a.Object.PedidoID == pedidoID).FirstOrDefault();
            await firebase.Child("Pedidos").Child(toDeletePedido.Key).DeleteAsync();

        }
        #endregion
        #region PedidoLineaTiempo
        public async Task<List<PedidoLineaTiempo>> GetAllPedidoLineaTiempos()
        {

            return (await firebase
              .Child("PedidoLineaTiempos")
              .OnceAsync<PedidoLineaTiempo>()).Select(item => new PedidoLineaTiempo
              {
                  PedidoLineaTiempoID = item.Object.PedidoLineaTiempoID,
                  PedidoID = item.Object.PedidoID,
                  EstadoPedidoID = item.Object.EstadoPedidoID,
                  Descripcion = item.Object.Descripcion,
                  Fecha = item.Object.Fecha,
                  FechaTexto = item.Object.FechaTexto,
                  Numero = item.Object.Numero,
                  UsuarioClienteID = item.Object.UsuarioClienteID
              }).ToList();
        }

        public async Task AddPedidoLineaTiempo(PedidoLineaTiempo pedido)
        {

            await firebase
              .Child("PedidoLineaTiempos")
              .PostAsync(new PedidoLineaTiempo()
              {
                  PedidoLineaTiempoID = pedido.PedidoLineaTiempoID,
                  PedidoID = pedido.PedidoID,
                  EstadoPedidoID = pedido.EstadoPedidoID,
                  Descripcion = pedido.Descripcion,
                  Fecha = pedido.Fecha,
                  FechaTexto = pedido.FechaTexto,
                  Numero = pedido.Numero,
                  UsuarioClienteID = pedido.UsuarioClienteID
              });
        }

        public async Task<PedidoLineaTiempo> GetPedidoLineaTiempo(int pedidoLineaTiempoID)
        {
            var allPedidoLineaTiempos = await GetAllPedidoLineaTiempos();
            await firebase
              .Child("PedidoLineaTiempos")
              .OnceAsync<PedidoLineaTiempo>();
            return allPedidoLineaTiempos.Where(a => a.PedidoLineaTiempoID == pedidoLineaTiempoID).FirstOrDefault();
        }

        public async Task UpdatePedidoLineaTiempo(int pedidoLineaTiempoID, string estado)
        {
            var toUpdatePedidoLineaTiempo = (await firebase
              .Child("PedidoLineaTiempos")
              .OnceAsync<PedidoLineaTiempo>()).Where(a => a.Object.PedidoLineaTiempoID == pedidoLineaTiempoID).FirstOrDefault();

            await firebase
              .Child("PedidoLineaTiempos")
              .Child(toUpdatePedidoLineaTiempo.Key)
              .PutAsync(new PedidoLineaTiempo() { PedidoLineaTiempoID = pedidoLineaTiempoID, EstadoPedidoID = estado });
        }

        public async Task DeletePedidoLineaTiempo(int pedidoLineaTiempoID)
        {
            var toDeletePedidoLineaTiempo = (await firebase
              .Child("PedidoLineaTiempos")
              .OnceAsync<PedidoLineaTiempo>()).Where(a => a.Object.PedidoLineaTiempoID == pedidoLineaTiempoID).FirstOrDefault();
            await firebase.Child("PedidoLineaTiempos").Child(toDeletePedidoLineaTiempo.Key).DeleteAsync();

        }
        #endregion
    }

}
using Axeso_DA;
using Axeso_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BL
{
    public class ModelsBL
    {
        #region Usuario
        private ModelsDA modelDAL = new ModelsDA();
        public async Task<List<UnidadMedida>> GetUnidadMedidas()
        {
            return await modelDAL.GetUnidadMedidas();
        }
        public async Task<List<Categoria>> GetCategorias()
        {
            return await modelDAL.GetCategorias();
        }
        public async Task<List<Usuarios>> Listar()
        {
            return await modelDAL.GetUsuarios();
        }
        public async Task<Usuarios> GetById(int id)
        {
            return await modelDAL.Get(id);
        }
        public async Task<Usuarios> Login(string usuario)
        {
            return await modelDAL.Login(usuario);
        }
        public async Task<Usuarios> CreateUsuario(UsuarioInput usuario)
        {
            return await modelDAL.CreateUsuario(usuario);
        }
        public async Task<Usuarios> UpdateUsuario(int usuarioid, UsuarioInput usuario)
        {
            return await modelDAL.UpdateUsuario(usuarioid, usuario);
        }

        public async Task<Usuarios> RecoverPassUsuario(int id, UsuarioInput usuario)
        {
            return await modelDAL.RecoverPassUsuario(id, usuario);
        }

        public EncryptedTokenModel EncriptaClave(string token)
        {
            return modelDAL.EncryptedToken(token);
        }
        public AnyStringModel GeneraPass()
        {
            return modelDAL.GeneraPass();
        }

        public Task<AnyBoolModel> CambiaClave(MailModel newmailmodel)
        {
            return modelDAL.CambiaClave(newmailmodel);
        }
        #endregion
        #region Productos

        public async Task<IEnumerable<ProductoMarca>> ListarProductoMarca()
        {
            return await modelDAL.GetAllProductoMarca();
        }

        public async Task<IEnumerable<Producto>> ListarProductos()
        {
            return await modelDAL.GetProductos();
        }
        public async Task<IEnumerable<Producto>> BuscaProducto(string nombre)
        {
            return await modelDAL.BuscaProducto(nombre);
        }
        public async Task<IEnumerable<Producto>> BuscaProductoId(int productoID)
        {
            return await modelDAL.BuscaProductoId(productoID);
        }
        public async Task<IEnumerable<Producto>> BuscaProductoCategoriaId(int categoriaID)
        {
            return await modelDAL.BuscaProductoCategoriaId(categoriaID);
        }
        public async Task<IEnumerable<Producto>> BuscaProductosByProductoMarcaID(int productoMarcaID)
        {
            return await modelDAL.BuscaProductosByProductoMarcaID(productoMarcaID);
        }
        #endregion

        #region Solicitud
        public async Task<IEnumerable<Solicitud>> GetSolicituds()
        {
            return await modelDAL.GetAllSolicitudes();
        }
        public async Task<IEnumerable<Solicitud>> GetAllSolicitudesByUsuario(int solicitudesusuario)
        {
            return await modelDAL.GetAllSolicitudesByUsuario(solicitudesusuario);
        }
        public async Task<IEnumerable<Solicitud>> GetAllSolicitudesByUsuarioID(int solicitudesusuarioID, int solicitudid)
        {
            return await modelDAL.GetAllSolicitudesByUsuarioID(solicitudesusuarioID, solicitudid);
        }
        public async Task<Solicitud> AddSolicitud(SolicitudInput solicitudToCreate)
        {
            return await modelDAL.AddSolicitud(solicitudToCreate);
        }
        #endregion

        #region SolicitudProducto
        public async Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProducto()
        {
            return await modelDAL.GetAllSolicitudesProducto();
        }
        public async Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProductoByUsuario(int solicitudesusuario)
        {
            return await modelDAL.GetAllSolicitudesProductoByUsuario(solicitudesusuario);
        }

        public async Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProductoByUsuarioID(int solicitudesproductousuarioID, int solicitudid)
        {
            return await modelDAL.GetAllSolicitudesProductoByUsuarioID(solicitudesproductousuarioID, solicitudid);
        }
        public async Task<SolicitudProducto> AddSolicitudProducto(SolicitudProductoInput solicitudproductoToCreate)
        {
            return await modelDAL.AddSolicitudProducto(solicitudproductoToCreate);
        }
        #endregion

        #region DataFarmacias
        public async Task<IEnumerable<DataFarmacias>> GetAllDataFarmacias()
        {
            return await modelDAL.GetAllDataFarmacias();
        }
        #endregion

        #region
        public async Task<IEnumerable<SolicitudDataFarmacias>> GetAllSolicitudDataFarmaciasBySolicitudID(int solicitudid)
        {
            return await modelDAL.GetAllSolicitudDataFarmaciasBySolicitudID(solicitudid);
        }
        public async Task<SolicitudDataFarmacias> AddSolicitudDataFarmacias(SolicitudDataFarmaciasInput solicitudDataFarmaciasToCreate)
        {
            return await modelDAL.AddSolicitudDataFarmacias(solicitudDataFarmaciasToCreate);
        }
        #endregion

        #region Cotizaciones
        public async Task<Cotizacion> UpdateCotizacion(Cotizacion cotizacionToUpdate)
        {
            return await modelDAL.UpdateCotizacion(cotizacionToUpdate);
        }
        public async Task<IEnumerable<Cotizacion>> GetAllCotizaciones()
        {
            return await modelDAL.GetAllCotizaciones();
        }
        public async Task<IEnumerable<Cotizacion>> GetAllCotizacionBySolicitud(int cotizacionsolicitudID)
        {
            return await modelDAL.GetAllCotizacionBySolicitud(cotizacionsolicitudID);
        }
        public async Task<Cotizacion> GetAllCotizacionById(int cotizacionID)
        {
            return await modelDAL.GetAllCotizacionById(cotizacionID);
        }
        public async Task<IEnumerable<Cotizacion>> GetAllCotizacionesByUsuarioID(int cotizacionusuarioID)
        {
            return await modelDAL.GetAllCotizacionesByUsuarioID(cotizacionusuarioID);
        }
        public async Task<IEnumerable<CotizacionProducto>> GetAllCotizacionProductosByCotizacionID(int cotizacionproductoFK)
        {
            return await modelDAL.GetAllCotizacionProductosByCotizacionID(cotizacionproductoFK);
        }

        #endregion

        #region UsuarioDireccion
        public async Task<IEnumerable<UsuarioDireccion>> GetAllUsuarioDireccion()
        {
            return await modelDAL.GetAllUsuarioDireccion();
        }

        public async Task<IEnumerable<UsuarioDireccion>> GetAllUsuarioDireccionByUsuarioID(int usuarioDireccionUsuarioID)
        {
            return await modelDAL.GetAllUsuarioDireccionByUsuarioID(usuarioDireccionUsuarioID);
        }
        public async Task<IEnumerable<UsuarioDireccion>> GetAllUsuarioDireccionEstablecimiento()
        {
            return await modelDAL.GetAllUsuarioDireccionEstablecimiento();
        }
        public async Task<UsuarioDireccion> GetAllUsuarioDireccionByID(int usuarioDireccionID)
        {
            return await modelDAL.GetAllUsuarioDireccionByID(usuarioDireccionID);
        }
        public async Task<UsuarioDireccion> AddUsuarioDireccion(UsuarioDireccionInput usuarioDireccionToCreate)
        {
            return await modelDAL.AddUsuarioDireccion(usuarioDireccionToCreate);
        }
        public async Task<UsuarioDireccion> UpdateUsuarioDireccion(UsuarioDireccionInputU usuarioDireccionToUpdate)
        {
            return await modelDAL.UpdateUsuarioDireccion(usuarioDireccionToUpdate);
        }
        public async Task<IEnumerable<UsuarioIngreso>> GetAllUsuarioIngreso()
        {
            return await modelDAL.GetAllUsuarioIngreso();
        }
        public async Task<UsuarioIngreso> GetAllUsuarioIngresoById(int usuarioIngresoID)
        {
            return await modelDAL.GetAllUsuarioIngresoById(usuarioIngresoID);
        }
        public async Task<UsuarioIngreso> AddUsuarioIngreso(UsuarioIngresoInput usuarioIngresoToCreate)
        {
            return await modelDAL.AddUsuarioIngreso(usuarioIngresoToCreate);
        }
        public async Task<UsuarioIngreso> UpdateUsuarioIngreso(UsuarioIngreso usuarioDireccionToUpdate)
        {
            return await modelDAL.UpdateUsuarioIngreso(usuarioDireccionToUpdate);
        }

        #endregion

        public async Task<IEnumerable<Parametro>> GetAllParametros()
        {
            return await modelDAL.GetAllParametros();
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidos()
        {
            return await modelDAL.GetAllPedidos();
        }
        public async Task<IEnumerable<Pedido>> GetAllPedidosByPedido(int pedidoID)
        {
            return await modelDAL.GetAllPedidosByPedido(pedidoID);
        }
        public async Task<IEnumerable<Pedido>> GetAllPedidosBySolicitud(int pedidosolicitudID)
        {
            return await modelDAL.GetAllPedidosBySolicitud(pedidosolicitudID);
        }
        public async Task<Pedido> AddPedido(PedidoInput pedidoToCreate)
        {
            return await modelDAL.AddPedido(pedidoToCreate);
        }
        public async Task<IEnumerable<PedidoProducto>> GetAllPedidoProductoByPedido(int pedidoproductopedidoID)
        {
            return await modelDAL.GetAllPedidoProductoByPedido(pedidoproductopedidoID);
        }
        public async Task<PedidoProducto> AddPedidoProducto(PedidoProductoInput pedidoProductoToCreate)
        {
            return await modelDAL.AddPedidoProducto(pedidoProductoToCreate);
        }
        public async Task<IEnumerable<PedidoLineaTiempo>> GetAllPedidoLineaTiempoByPedido(int pedidoLineaTiemposID)
        {
            return await modelDAL.GetAllPedidoLineaTiempoByPedido(pedidoLineaTiemposID);
        }

        public async Task<IEnumerable<Notificacion>> GetAllNotificacionByUsuario(int notificacionusuarioID)
        {
            return await modelDAL.GetAllNotificacionByUsuario(notificacionusuarioID);
        }

        public async Task<IEnumerable<Distrito>> GetAllDistritos()
        {
            return await modelDAL.GetAllDistritos();
        }

        #region Nodejs
        public async Task<Usuarios> ValidaLogueo(Usuarios usuario)
        {
            return await modelDAL.ValidaLogueo(usuario);
        }
        #endregion
    }
}

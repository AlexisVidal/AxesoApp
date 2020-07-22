using Axeso_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Axeso_SL.Interfaces
{
    public interface IRepository
    {
        #region Cultura
        Task<List<Cultura>> GetCulturas();
        Task<Cultura> GetCulturaById(int id);
        #endregion
        #region UsuarioModel
        Task<List<Usuarios>> GetUsuarios();
        Usuarios GetUsuarioById(int id);
        Task<Usuarios> GetUsuarioByLogin(string usuario);
        Task<Usuarios> Add(Usuarios usuario);
        Task<Usuarios> UpdateToken(Usuarios usuario, Usuarios usuarioID);
        #endregion
        #region TipoProductoTipoNegocio
        Task<List<TipoProductoTipoNegocio>> GetTipoProductoTipoNegocio();
        Task<ILookup<int, TipoProductoTipoNegocio>> GetTipoProductoTipoNegocioByTipoProductoIDs(IEnumerable<int> tipoProductoIDs);
        Task<List<TipoProductoTipoNegocio>> GetAllTipoProductoTipoNegocio();
        #endregion
        #region TipoProducto
        Task<List<TipoProducto>> GetTipoProductoModelByTpTn(int tipoProductoID);
        Task<ILookup<int, TipoProducto>> GetTipoProductosByTipoProductoIds(IEnumerable<int> tipoProductoIDs);
        Task<IEnumerable<TipoProducto>> GetAllTipoProducto();
        #endregion
        #region Categoria
        Task<List<Categoria>> GetAllCategorias();
        Task<ILookup<int, Categoria>> GetCategoriaIds(IEnumerable<int> categoriaIDs);
        Task<Categoria> GetCategoriaOneIds(int categoriaIDs);
        #endregion
        #region Producto
        Task<IEnumerable<ProductoMarca>> GetAllProductoMarca();
        Task<IEnumerable<Producto>> GetAllProductos();
        Task<IEnumerable<Producto>> GetAllProductosByNombre(string nombre);
        Task<IEnumerable<Producto>> GetAllProductosByProductoID(int productoID);
        Task<IEnumerable<Producto>> GetAllProductosByCategoriaID(int productoscategoriaID);
        Task<IEnumerable<Producto>> GetAllProductosByProductoMarcaID(int productoMarcaID);
        #endregion
        #region TipoNegocio
        Task<ILookup<int, TipoNegocio>> GetTipoNegocioIds(IEnumerable<int> tipoNegocioIDs);
        #endregion
        #region UnidadMedida
        Task<List<UnidadMedida>> GetAllUnidadMedidas();
        Task<ILookup<int, UnidadMedida>> GetUnidadMedidaIds(IEnumerable<int> unidadIDs);
        #endregion

        #region Solicitud
        Task<IEnumerable<Solicitud>> GetAllSolicitudes();
        Task<IEnumerable<Solicitud>> GetAllSolicitudesByUsuario(int usuario);
        Task<Cotizacion> Updatecotizacion(Cotizacion cotizacion);
        Task<IEnumerable<Cotizacion>> GetAllCotizacionBySolicitud(int solicitudID);
        Task<IEnumerable<Cotizacion>> GetAllCotizacionById(int cotizacionID);
        Task<IEnumerable<Solicitud>> GetAllSolicitudesByUsuarioID(int usuario, int solicitudid);
        Task<Solicitud> AddSolicitud(Solicitud solicitud);
        #endregion

        #region SolicitudProducto
        Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProducto();
        Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProductoByUsuario(int usuario);
        Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProductoByUsuarioID(int usuario, int solicitudid);
        Task<SolicitudProducto> AddSolicitudProducto(SolicitudProducto solicitudproducto);
        #endregion

        #region DataFarmacias
        Task<IEnumerable<DataFarmacias>> GetAllDataFarmacias();
        #endregion

        #region SolicitudDataFarmacias
        Task<IEnumerable<SolicitudDataFarmacias>> GetAllSolicitudDataFarmacias();
        Task<IEnumerable<SolicitudDataFarmacias>> GetAllSolicitudDataFarmaciasByUsuario(int usuario);
        Task<IEnumerable<SolicitudDataFarmacias>> GetAllSolicitudDataFarmaciasBySolicitudID(int solicitudid);
        Task<IEnumerable<SolicitudDataFarmacias>> GetAllSolicitudDataFarmaciasByUsuarioID(int usuario, int solicitudid);
        Task<SolicitudDataFarmacias> AddSolicitudDataFarmacias(SolicitudDataFarmacias solicitudDataFarmacias);
        #endregion

        #region DataFarmacias
        Task<IEnumerable<Cotizacion>> GetAllCotizacion();
        Task<IEnumerable<Cotizacion>> GetAllCotizacionByUsuario(int usuario);
        Task<IEnumerable<CotizacionProducto>> GetAllCotizacionProductoByFk(int cotizacionid);
        //Task<IEnumerable<CotizacionProducto>> GetAllCotizacionProductoByFk(int cotizacionid);

        #endregion

        #region DataFarmacias
        Task<IEnumerable<UsuarioDireccion>> GetAllUsuarioDireccion();
        Task<IEnumerable<UsuarioDireccion>> GetAllUsuarioDireccionByUsuario(int usuario);
        UsuarioDireccion GetAllUsuarioDireccionById(int usuariodireccionid);
        Task<UsuarioDireccion> AddUsuarioDireccion(UsuarioDireccion usuarioDireccion);
        Task<UsuarioDireccion> UpdateUsuarioDireccion(UsuarioDireccion usuariodirec, UsuarioDireccion usuariodirnew);
        Task<IEnumerable<UsuarioIngreso>> GetAllUsuarioIngreso();
        UsuarioIngreso GetAllUsuarioIngresoById(int usuarioIngresoID);
        Task<UsuarioIngreso> AddUsuarioIngreso(UsuarioIngreso usuarioIngreso);

        Task<UsuarioIngreso> UpdateUsuarioIngreso(UsuarioIngreso usuarioIngreso, UsuarioIngreso usuarioIngresonew);

        Task<IEnumerable<Parametro>> GetAllParametros();

        #endregion
        #region Pedidos
        Task<IEnumerable<Pedido>> GetAllPedidos();
        Task<IEnumerable<Pedido>> GetAllPedidosByPedido(int pedidoID);
        Task<IEnumerable<Pedido>> GetAllPedidosBySolicitud(int solicitudID);
        Task<Pedido> AddPedido(Pedido pedido);
        Task<IEnumerable<PedidoProducto>> GetAllPedidoProductoByPedido(int pedidoID);
        Task<PedidoProducto> AddPedidoProducto(PedidoProducto pedidoProducto);
        #endregion

        Task<IEnumerable<PedidoLineaTiempo>> GetAllPedidoLineaTiempoByPedido(int pedidoID);
        Task<IEnumerable<Notificacion>> GetAllNotificacionByUsuario(int usuario);
    }
}

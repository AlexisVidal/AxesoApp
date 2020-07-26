using Axeso_SL.Interfaces;
using Axeso_BE;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.ExceptionServices;
using Mindbox.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Axeso_SL.Repositories
{
    public class Repository : IRepository
    {
        private readonly GraphQLDemoContext _context;
        public Repository(GraphQLDemoContext context)
        {
            _context = context;
        }
        #region Cultura
        public Task<List<Cultura>> GetCulturas()
        {
            return _context.Cultura.ToListAsync();
        }
        public Task<Cultura> GetCulturaById(int id)
        {
            return _context.Cultura.SingleAsync(a => a.ID == id);
        }
        #endregion
        #region Usuario
        public Task<List<Usuarios>> GetUsuarios()
        {

            try
            {
                return _context.Usuarios.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Usuarios GetUsuarioById(int id)
        {
            return GetUsuarios().Result.FirstOrDefault(p => p.UsuarioID == id); //_context.Usuario.SingleAsync(a => a.UsuarioID == id);
        }
        public Task<Usuarios> GetUsuarioByLogin(string usuario)
        {
            return Task.FromResult(GetUsuarios().Result.FirstOrDefault(p => p.Usuario == usuario)); //return _context.Usuario.SingleAsync(a => a.Usuario.Equals(usuario));
        }
        public async Task<Usuarios> Add(Usuarios usuario)
        {
            try
            {
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return usuario;
        }
        public async Task<Usuarios> UpdateToken(Usuarios usuario, Usuarios usuarioID)
        {
            try
            {
                usuario.Token = usuarioID.Token;
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return usuario;
        }
        #endregion        
        #region TipoNegocio
        public async Task<ILookup<int, TipoNegocio>> GetTipoNegocioIds(IEnumerable<int> tipoNegocioIDs)
        {
            List<TipoNegocio> list = null;
            try
            {
                list = await _context.TipoNegocio.Where(a => tipoNegocioIDs.Contains(a.TipoNegocioID)).ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return list.ToLookup(x => x.TipoNegocioID);
        }
        public Task<List<TipoProducto>> GetTipoProductoModelByTpTn(int tipoProductoID) => _context.TipoProducto
            .Where(a => a.TipoProductoID == tipoProductoID)
            .ToListAsync();
        public async Task<ILookup<int, TipoProducto>> GetTipoProductosByTipoProductoIds(IEnumerable<int> tipoProductoIDs)
        {
            List<TipoProducto> tiposproductos = null;
            try
            {
                var first = await _context.TipoProducto.ToListAsync();
                tiposproductos = new List<TipoProducto>();
                foreach (var item in tipoProductoIDs)
                {
                    var existen = first.Where(x => x.TipoProductoID == item).ToList();
                    if (existen.Any())
                    {
                        foreach (var itemx in existen)
                        {
                            tiposproductos.Add(itemx);
                        }

                    }
                }
                //tiposproductos = await first.Where(a => tipoProductoIDs.Contains(a.TipoProductoFK)).ToList();
            }
            catch (Exception ex)
            {

            }
            return tiposproductos.ToLookup(x => x.TipoProductoID);
        }
        #endregion
        #region TipoProductoTipoNegocio
        public async Task<List<TipoProductoTipoNegocio>> GetAllTipoProductoTipoNegocio()
        {
            List<TipoProductoTipoNegocio> list = null;
            try
            {
                list = await _context.TipoProductoTipoNegocio.ToListAsync();

            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public async Task<ILookup<int, TipoProductoTipoNegocio>> GetTipoProductoTipoNegocioByTipoProductoIDs(IEnumerable<int> tipoProductoIDs)
        {
            List<TipoProductoTipoNegocio> list = null;
            try
            {
                list = await _context.TipoProductoTipoNegocio.Where(a => tipoProductoIDs.Contains(a.TipoProductoID)).ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return list.ToLookup(x => x.TipoProductoID);
        }
        public Task<List<TipoProductoTipoNegocio>> GetTipoProductoTipoNegocio()
        {
            var data = _context.TipoProductoTipoNegocio.ToListAsync();
            return data;
        }
        #endregion
        #region Categoria
        public Task<List<Categoria>> GetAllCategorias()
        {
            try
            {
                var data = _context.Categoria.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ILookup<int, Categoria>> GetCategoriaIds(IEnumerable<int> categoriaIDs)
        {
            List<Categoria> list = null;
            try
            {
                list = new List<Categoria>();
                var all = await _context.Categoria.ToListAsync();
                foreach (var item in categoriaIDs)
                {
                    var existen = all.Where(x => x.CategoriaID == item).ToList();
                    if (existen.Any())
                    {
                        foreach (var itemx in existen)
                        {
                            list.Add(itemx);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return list.ToLookup(x => x.CategoriaID);
        }
        public async Task<Categoria> GetCategoriaOneIds(int categoriaIDs)
        {
            Categoria list = null;
            try
            {
                list = new Categoria();
                var all = await _context.Categoria.ToListAsync();
                var existen = all.Where(x => x.CategoriaID == categoriaIDs).FirstOrDefault();
                if (existen != null)
                {
                    list = existen;
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        #endregion
        #region UnidadMedida
        public Task<List<UnidadMedida>> GetAllUnidadMedidas()
        {
            var data = _context.UnidadMedida.ToListAsync();
            return data;
        }
        public async Task<ILookup<int, UnidadMedida>> GetUnidadMedidaIds(IEnumerable<int> unidadID_coms)
        {
            List<UnidadMedida> list = null;
            try
            {
                list = new List<UnidadMedida>();
                var all = await _context.UnidadMedida.ToListAsync();
                foreach (var item in unidadID_coms)
                {
                    var existen = all.Where(x => x.UnidadID == item).ToList();
                    if (existen.Any())
                    {
                        foreach (var itemx in existen)
                        {
                            list.Add(itemx);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return list.ToLookup(x => x.UnidadID);
        }
        #endregion
        #region TipoProducto
        public async Task<IEnumerable<TipoProducto>> GetAllTipoProducto() => _context.TipoProducto.ToList();
        #endregion
        #region Productos

        public async Task<IEnumerable<ProductoMarca>> GetAllProductoMarca()
        {
            return await _context
                .ProductoMarca
                .Where(x => x.Activo)
                .OrderBy(x => x.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetAllProductos()
        {
            return await _context
                .Producto
                .Include(x => x.Categoria)
                .Include(x => x.UnidadMedida)
                .ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetAllProductosByNombre(string nombre)
        {
            var queryParts = nombre.Split(' ');
            var xyz = await _context
                .Producto
                .Include(x => x.Categoria)
                .Include(x => x.UnidadMedida)
                //.Where(x => x.Busqueda.ToLower().Contains("ambr*' and '*250*'"))
                .Where(x => queryParts.All(p => x.Busqueda.ToLower().Contains(p)))
                //.Where(x=>x.Nombre.ToLower().Contains(nombre.ToLower())
                //        || x.Abreviatura.ToLower().Contains(nombre.ToLower())
                //        || x.Categoria.Nombre.ToLower().Contains(nombre.ToLower()))
                .ToListAsync();
            return xyz;
        }
        public async Task<IEnumerable<Producto>> GetAllProductosByProductoID(int productoID)
        {
            return await _context
                .Producto
                .Include(x => x.Categoria)
                .Include(x => x.UnidadMedida)
                .Where(x => x.ProductoID == productoID)
                .ToListAsync();
        }
        public async Task<IEnumerable<Producto>> GetAllProductosByCategoriaID(int productoscategoriaID)
        {
            return await _context
                .Producto
                .Include(x => x.Categoria)
                .Include(x => x.UnidadMedida)
                .Where(x => x.Categoria.CategoriaID == productoscategoriaID)
                .Take(100)
                .ToListAsync();
        }
        public async Task<IEnumerable<Producto>> GetAllProductosByProductoMarcaID(int productoMarcaID)
        {
            return await _context
                .Producto
                .Include(x => x.Categoria)
                .Include(x => x.UnidadMedida)
                .Where(x => x.ProductoMarcaID == productoMarcaID)
                .ToListAsync();
        }
        #endregion

        #region Solicitudes
        public async Task<IEnumerable<Solicitud>> GetAllSolicitudes()
        {
            var solicituds = await _context
                .Solicitud
                .Include(x => x.SolicitudProducto)
                .Include(x => x.SolicitudDataFarmacias)
                .ToListAsync();
            return solicituds;
        }

        public async Task<IEnumerable<Solicitud>> GetAllSolicitudesByUsuario(int usuario)
        {
            try
            {
                var xyz = await _context
                .Solicitud
                .Include(x => x.SolicitudProducto)
                .Include(x => x.SolicitudDataFarmacias)
                .Include(x => x.Usuarios)
                .Where(x => x.UsuarioID == usuario)
                .ToListAsync();
                return xyz;
            }
            catch (Exception ex)
            {
                return new List<Solicitud>();
            }
        }
        public async Task<IEnumerable<Solicitud>> GetAllSolicitudesByUsuarioID(int usuario, int solicitudid)
        {
            return await _context
                .Solicitud
                .Include(x => x.SolicitudProducto)
                .Include(x => x.SolicitudDataFarmacias)
                .Include(x => x.Usuarios)
                .Where(x => x.UsuarioID == usuario && x.SolicitudID == solicitudid)
                .ToListAsync();
        }
        public async Task<Solicitud> AddSolicitud(Solicitud solicitud)
        {
            try
            {
                await _context.Solicitud.AddAsync(solicitud);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return solicitud;
        }
        #endregion
        #region SolicitudProducto
        public async Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProducto()
        {
            return await _context
                .SolicitudProducto
                .Include(x => x.Solicitud)
                .ToListAsync();
        }

        public async Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProductoByUsuario(int usuario)
        {
            var xyz = await _context
                .SolicitudProducto
                .Include(x => x.Solicitud)
                .Where(x => x.Solicitud.UsuarioID == usuario)
                .ToListAsync();
            return xyz;
        }
        public async Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProductoByUsuarioID(int usuario, int solicitudid)
        {
            return await _context
                .SolicitudProducto
                .Include(x => x.Solicitud)
                .Where(x => x.Solicitud.UsuarioID == usuario && x.SolicitudID == solicitudid)
                .ToListAsync();
        }
        public async Task<SolicitudProducto> AddSolicitudProducto(SolicitudProducto solicitudproducto)
        {
            try
            {
                await _context.SolicitudProducto.AddAsync(solicitudproducto);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return solicitudproducto;
        }
        #endregion
        #region DataFarmacias
        public async Task<IEnumerable<DataFarmacias>> GetAllDataFarmacias()
        {
            return await _context
                .DataFarmacias
                .Where(q => q.Latitud != 0)
                .OrderBy(x => x.Departamento)
                .OrderBy(y => y.Provincia)
                .OrderBy(z => z.Distrito)
                .ToListAsync();
        }
        #endregion


        #region SolicitudDataFarmacias
        public async Task<IEnumerable<SolicitudDataFarmacias>> GetAllSolicitudDataFarmacias()
        {
            return await _context
                .SolicitudDataFarmacias
                .Include(x => x.Solicitud)
                .Include(x => x.Usuarios)
                .Include(x => x.Usuarios.UsuarioDireccion)
                .ToListAsync();
        }
        public async Task<IEnumerable<SolicitudDataFarmacias>> GetAllSolicitudDataFarmaciasBySolicitudID(int solicitudid)
        {
            var xyz = await _context
                .SolicitudDataFarmacias
                .Include(x => x.Solicitud)
                .Include(x => x.Usuarios)
                .Include(x => x.Usuarios.UsuarioDireccion)
                .Where(x => x.SolicitudID == solicitudid)
                .ToListAsync();
            return xyz;
        }
        public async Task<IEnumerable<SolicitudDataFarmacias>> GetAllSolicitudDataFarmaciasByUsuario(int usuario)
        {
            var xyz = await _context
                .SolicitudDataFarmacias
                .Include(x => x.Solicitud)
                .Include(x => x.Usuarios)
                .Include(x => x.Usuarios.UsuarioDireccion)
                .Where(x => x.Solicitud.UsuarioID == usuario)
                .ToListAsync();
            return xyz;
        }
        public async Task<IEnumerable<SolicitudDataFarmacias>> GetAllSolicitudDataFarmaciasByUsuarioID(int usuario, int solicitudid)
        {
            return await _context
                .SolicitudDataFarmacias
                .Include(x => x.Solicitud)
                .Include(x => x.Usuarios)
                .Include(x => x.Usuarios.UsuarioDireccion)
                .Where(x => x.Solicitud.UsuarioID == usuario && x.SolicitudID == solicitudid)
                .ToListAsync();
        }
        public async Task<SolicitudDataFarmacias> AddSolicitudDataFarmacias(SolicitudDataFarmacias solicitudDataFarmacias)
        {
            try
            {
                await _context.SolicitudDataFarmacias.AddAsync(solicitudDataFarmacias);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return solicitudDataFarmacias;
        }
        #endregion

        #region DataFarmacias
        public async Task<IEnumerable<Cotizacion>> GetAllCotizacion()
        {
            return await _context
                .Cotizacion
                .Include(x => x.Solicitud)
                .Include(x => x.Usuarios)
                .Include(x => x.Usuarios.UsuarioDireccion)
                .Where(q => q.Activo)
                .OrderBy(x => x.Fecha)
                .ToListAsync();
        }
        public async Task<IEnumerable<Cotizacion>> GetAllCotizacionByUsuario(int usuario)
        {
            var xyz = await _context
                .Cotizacion
                .Include(x => x.Solicitud)
                .Include(x => x.Usuarios)
                .Include(x => x.CotizacionProducto)
                .Include(x => x.Usuarios.UsuarioDireccion)
                .Where(x => x.Activo && x.UsuarioID == usuario)
                .ToListAsync();
            return xyz;
        }
        public async Task<IEnumerable<Cotizacion>> GetAllCotizacionById(int cotizacionID)
        {
            var xyz = await _context
                .Cotizacion
                .Include(x => x.Solicitud)
                .Include(x => x.Usuarios)
                .Include(x => x.CotizacionProducto)
                .Include(x => x.Usuarios.UsuarioDireccion)
                .Where(x => x.Activo && x.CotizacionID == cotizacionID)
                .ToListAsync();
            return xyz;
        }
        public async Task<Cotizacion> Updatecotizacion(Cotizacion cotizacion)
        {
            try
            {
                _context.Cotizacion.Update(cotizacion);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return cotizacion;
        }
        public async Task<IEnumerable<Cotizacion>> GetAllCotizacionBySolicitud(int solicitudID)
        {
            var xyz = await _context
                .Cotizacion
                .Include(x => x.Solicitud)
                .Include(x => x.Usuarios)
                .Include(x => x.CotizacionProducto)
                .Include(x => x.Usuarios.UsuarioDireccion)
                .Where(x => x.Activo && x.SolicitudID == solicitudID)
                .ToListAsync();
            return xyz;
        }
        public async Task<IEnumerable<CotizacionProducto>> GetAllCotizacionProductoByFk(int cotizacionid)
        {
            var xyz = await _context
                .CotizacionProducto
                .Include(x => x.SolicitudProducto)
                .Include(x => x.Cotizacion)
                .Include(x => x.Cotizacion.Solicitud)
                .Include(x => x.Cotizacion.Usuarios)
                .Where(x => x.CotizacionID == cotizacionid)
                .ToListAsync();
            return xyz;
        }

        #endregion

        #region Distrito
        public async Task<IEnumerable<Distrito>> GetAllDistritos()
        {
            return await _context
                .Distrito
                .Where(q => q.Estado.Equals("1"))
                .ToListAsync();
        }
        #endregion
        #region UsuarioDireccion
        public async Task<IEnumerable<UsuarioDireccion>> GetAllUsuarioDireccion()
        {
            return await _context
                .UsuarioDireccion
                .Include(x=>x.Distrito)
                .Where(q => q.Activo)
                .ToListAsync();
        }
        public async Task<IEnumerable<UsuarioDireccion>> GetAllUsuarioDireccionByUsuario(int usuario)
        {
            var xyz = await _context
                .UsuarioDireccion
                .Include(x => x.Distrito)
                .Where(x => x.Activo && x.UsuarioID == usuario)
                .ToListAsync();
            return xyz;
        }
        public UsuarioDireccion GetAllUsuarioDireccionById(int usuariodireccionid)
        {
            var xyz = _context
                .UsuarioDireccion
                .Include(x => x.Distrito)
                .Where(x => x.Activo && x.UsuarioDireccionID == usuariodireccionid)
                .FirstOrDefault();
            return xyz;
        }
        public async Task<UsuarioDireccion> AddUsuarioDireccion(UsuarioDireccion usuarioDireccion)
        {
            try
            {
                await _context.UsuarioDireccion.AddAsync(usuarioDireccion);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return usuarioDireccion;
        }

        public async Task<UsuarioDireccion> UpdateUsuarioDireccion(UsuarioDireccion usuariodirec, UsuarioDireccion usuariodirnew)
        {
            try
            {
                usuariodirec.Nombre = usuariodirnew.Nombre;
                usuariodirec.Direccion = usuariodirnew.Direccion;
                usuariodirec.Latitud = usuariodirnew.Latitud;
                usuariodirec.Longitud = usuariodirnew.Longitud;
                usuariodirec.Activo = usuariodirnew.Activo;
                usuariodirec.UsuarioID = usuariodirnew.UsuarioID;
                usuariodirec.DistritoID = usuariodirnew.DistritoID;
                usuariodirec.Departamento = usuariodirnew.Departamento;
                _context.UsuarioDireccion.Update(usuariodirec);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return usuariodirec;
        }
        public async Task<IEnumerable<UsuarioIngreso>> GetAllUsuarioIngreso()
        {
            return await _context
                .UsuarioIngreso
                .Where(q => q.Disponible)
                .ToListAsync();
        }
        public UsuarioIngreso GetAllUsuarioIngresoById(int usuarioIngresoID)
        {
            var xyz = _context
                .UsuarioIngreso
                .Where(x => x.Disponible && x.UsuarioID == usuarioIngresoID)
                .FirstOrDefault();
            return xyz;
        }

        public async Task<UsuarioIngreso> AddUsuarioIngreso(UsuarioIngreso usuarioIngreso)
        {
            try
            {
                await _context.UsuarioIngreso.AddAsync(usuarioIngreso);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return usuarioIngreso;
        }

        public async Task<UsuarioIngreso> UpdateUsuarioIngreso(UsuarioIngreso usuarioIngreso, UsuarioIngreso usuarioIngresonew)
        {
            try
            {
                usuarioIngreso.FechaUltimaActualizacion = usuarioIngresonew.FechaUltimaActualizacion;
                usuarioIngreso.Disponible = usuarioIngresonew.Disponible;
                usuarioIngreso.UsuarioIngresoID = usuarioIngresonew.UsuarioIngresoID;
                _context.UsuarioIngreso.Update(usuarioIngreso);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return usuarioIngreso;
        }

        public async Task<IEnumerable<Parametro>> GetAllParametros()
        {
            return await _context
                .Parametro
                .Where(x => x.Activo)
                .ToListAsync();
        }

        #endregion
        #region Pedidos
        public async Task<IEnumerable<Pedido>> GetAllPedidos()
        {
            var pedidos = await _context
                .Pedido
                .Where(x => x.Activo)
                .ToListAsync();
            return pedidos;
        }
        public async Task<IEnumerable<Pedido>> GetAllPedidosByPedido(int pedidoID)
        {
            var pedidos = await _context
                .Pedido
                .Where(x => x.Activo && x.PedidoID == pedidoID)
                .ToListAsync();
            return pedidos;
        }
        public async Task<IEnumerable<Pedido>> GetAllPedidosBySolicitud(int solicitudID)
        {
            var pedidos = await _context
                .Pedido
                .Where(x => x.Activo && x.SolicitudID == solicitudID)
                .ToListAsync();
            return pedidos;
        }
        public async Task<Pedido> AddPedido(Pedido pedido)
        {
            try
            {
                await _context.Pedido.AddAsync(pedido);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return pedido;
        }

        public async Task<IEnumerable<PedidoProducto>> GetAllPedidoProductoByPedido(int pedidoID)
        {
            var pedidoproductos = await _context
                .PedidoProducto
                .Where(x => x.Activo && x.PedidoID == pedidoID)
                .ToListAsync();
            return pedidoproductos;
        }
        public async Task<PedidoProducto> AddPedidoProducto(PedidoProducto pedidoProducto)
        {
            try
            {
                await _context.PedidoProducto.AddAsync(pedidoProducto);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return pedidoProducto;
        }
        #endregion

        public async Task<IEnumerable<PedidoLineaTiempo>> GetAllPedidoLineaTiempoByPedido(int pedidoID)
        {
            var pedidoLineaTiempos = await _context
                .PedidoLineaTiempo
                .Where(x => x.PedidoID == pedidoID)
                .ToListAsync();
            return pedidoLineaTiempos;
        }

        public async Task<IEnumerable<Notificacion>> GetAllNotificacionByUsuario(int usuario)
        {
            var notificacionusuario = await _context
                .Notificacion
                .Where(x => x.UsuarioID == usuario)
                .ToListAsync();
            return notificacionusuario;
        }

    }
}

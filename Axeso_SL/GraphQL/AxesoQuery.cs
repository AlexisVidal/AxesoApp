using Axeso_SL.Interfaces;
using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;

namespace Axeso_SL.GraphQL
{
    public partial class AxesoQuery : ObjectGraphType
    {
        public AxesoQuery(IRepository repository)
        {
            #region UsuarioType
            Field<ListGraphType<UsuarioType>>(
                "usuarios",
                resolve: context => repository.GetUsuarios());

            //Field<UsuarioType>(
            //    "usuario",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
            //    { Name = "usuario" }),
            //    resolve: context =>
            //    {
            //        var id = context.GetArgument<string>("usuario");
            //        return repository.GetUsuarioByLogin(id);
            //    }
            //);
            Field<UsuarioType>(
                "login",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>>
                { Name = "login" }),
                resolve: context =>
                {
                    var usuario = context.GetArgument<string>("login");
                    return repository.GetUsuarioByLogin(usuario);
                }
            );
            Field<UsuarioType>(
                "usuariobyid",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "usuarioId" }),
                resolve: context =>
                {
                    int id = context.GetArgument<int>("usuarioId");
                    return repository.GetUsuarioById(id);
                }
            );
            #endregion
            #region CulturaType
            Field<ListGraphType<CulturaType>>(
                "culturas",
                resolve: context => repository.GetCulturas());

            Field<CulturaType>(
                "cultura",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return repository.GetCulturaById(id);
                }
            );
            #endregion
            #region TipoProductoTipoNegocioType
            Field<ListGraphType<TipoProductoTipoNegocioType>>(
                "tipoproductotiponegocios",
                resolve: context => repository.GetTipoProductoTipoNegocio());
            #endregion
            #region TipoProductoType
            Field<ListGraphType<TipoProductoType>>(
               "tipoProductos",
               resolve: context => repository.GetAllTipoProducto()
            );
            Field<ListGraphType<TipoProductoType>>(
               "tipoProductoTipoNegocio",
               resolve: context => repository.GetAllTipoProductoTipoNegocio()
            );
            #endregion
            #region CategoriaType
            Field<ListGraphType<CategoriaType>>(
                "categorias",
                resolve: context => repository.GetAllCategorias());
            #endregion
            #region UnidadMedidaType
            Field<ListGraphType<UnidadMedidaType>>(
                "unidadMedidas",
                resolve: context => repository.GetAllUnidadMedidas());
            #endregion
            #region ProductoType
            Field<ListGraphType<ProductoMarcaType>>(
                "productomarcas",
                resolve: context => repository.GetAllProductoMarca());

            Field<ListGraphType<ProductoType>>(
                "productos",
                resolve: context => repository.GetAllProductos());

            Field<ListGraphType<ProductoType>>(
               "productosnombre",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>>
               { Name = "productosnombre" }),
               resolve: context =>
               {
                   var nombre = context.GetArgument<string>("productosnombre");
                   return repository.GetAllProductosByNombre(nombre);
               }
           );
            Field<ListGraphType<ProductoType>>(
               "productosproductoID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "productosproductoID" }),
               resolve: context =>
               {
                   var productoID = context.GetArgument<int>("productosproductoID");
                   return repository.GetAllProductosByProductoID(productoID);
               }
           );
            Field<ListGraphType<ProductoType>>(
               "productoscategoriaID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "productoscategoriaID" }),
               resolve: context =>
               {
                   var categoriaID = context.GetArgument<int>("productoscategoriaID");
                   return repository.GetAllProductosByCategoriaID(categoriaID);
               }
           );
            Field<ListGraphType<ProductoType>>(
               "productoMarcaID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "productoMarcaID" }),
               resolve: context =>
               {
                   var productoMarcaID = context.GetArgument<int>("productoMarcaID");
                   return repository.GetAllProductosByProductoMarcaID(productoMarcaID);
               }
           );
            #endregion

            #region SolicitudType
            Field<ListGraphType<SolicitudType>>(
                "solicitudes",
                resolve: context => repository.GetAllSolicitudes());

            Field<ListGraphType<SolicitudType>>(
               "solicitudesusuario",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "solicitudesusuario" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("solicitudesusuario");
                   return repository.GetAllSolicitudesByUsuario(usuario);
               }
           );
            Field<ListGraphType<SolicitudType>>(
               "solicitudesusuarioID",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "solicitudesusuarioID" },
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "solicitudid" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("solicitudesusuarioID");
                   var solicitudid = context.GetArgument<int>("solicitudid");
                   return repository.GetAllSolicitudesByUsuarioID(usuario, solicitudid);
               }
           );
            #endregion
            #region SolicitudProductoType
            Field<ListGraphType<SolicitudProductoType>>(
                "solicitudesproducto",
                resolve: context => repository.GetAllSolicitudesProducto());

            Field<ListGraphType<SolicitudProductoType>>(
               "solicitudesproductousuario",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "solicitudesproductousuario" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("solicitudesproductousuario");
                   return repository.GetAllSolicitudesProductoByUsuario(usuario);
               }
           );
            Field<ListGraphType<SolicitudProductoType>>(
               "solicitudesproductousuarioID",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "solicitudesproductousuarioID" },
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "solicitudid" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("solicitudesproductousuarioID");
                   var solicitudid = context.GetArgument<int>("solicitudid");
                   return repository.GetAllSolicitudesProductoByUsuarioID(usuario, solicitudid);
               }
           );

            #endregion
            #region DataFarmaciasType
            Field<ListGraphType<DataFarmaciasType>>(
                "datafarmacias",
                resolve: context => repository.GetAllDataFarmacias());
            #endregion

            #region SolicitudDataFarmaciasType
            Field<ListGraphType<SolicitudDataFarmaciasType>>(
                "solicitudDataFarmacias",
                resolve: context => repository.GetAllSolicitudDataFarmacias());

            Field<ListGraphType<SolicitudDataFarmaciasType>>(
               "solicitudDataFarmaciassolicitudid",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "solicitudDataFarmaciassolicitudid" }),
               resolve: context =>
               {
                   var solicitudid = context.GetArgument<int>("solicitudDataFarmaciassolicitudid");
                   return repository.GetAllSolicitudDataFarmaciasBySolicitudID(solicitudid);
               }
           );

            Field<ListGraphType<SolicitudDataFarmaciasType>>(
               "solicitudDataFarmaciasusuario",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "solicitudDataFarmaciasusuario" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("solicitudDataFarmaciasusuario");
                   return repository.GetAllSolicitudDataFarmaciasByUsuario(usuario);
               }
           );

            Field<ListGraphType<SolicitudDataFarmaciasType>>(
               "solicitudDataFarmaciasusuarioID",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "solicitudDataFarmaciasusuarioID" },
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "solicitudid" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("solicitudDataFarmaciasusuarioID");
                   var solicitudid = context.GetArgument<int>("solicitudid");
                   return repository.GetAllSolicitudDataFarmaciasByUsuarioID(usuario, solicitudid);
               }
           );

            #endregion

            #region Cotizacion
            Field<ListGraphType<CotizacionType>>(
                "cotizacion",
                resolve: context => repository.GetAllCotizacion());

            Field<ListGraphType<CotizacionType>>(
               "cotizacionusuarioID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "cotizacionusuarioID" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("cotizacionusuarioID");
                   return repository.GetAllCotizacionByUsuario(usuario);
               }
           );
            Field<ListGraphType<CotizacionType>>(
               "cotizacionsolicitudID",
               arguments: new QueryArguments(new QueryArgument<IntGraphType>
               { Name = "cotizacionsolicitudID" }),
               resolve: context =>
               {
                   var solicitudID = context.GetArgument<int>("cotizacionsolicitudID");
                   return repository.GetAllCotizacionBySolicitud(solicitudID);
               }
           );

            Field<ListGraphType<CotizacionType>>(
               "cotizacionID",
               arguments: new QueryArguments(new QueryArgument<IntGraphType>
               { Name = "cotizacionID" }),
               resolve: context =>
               {
                   var solicitudID = context.GetArgument<int>("cotizacionID");
                   return repository.GetAllCotizacionById(solicitudID);
               }
           );

            Field<ListGraphType<CotizacionProductoType>>(
               "cotizacionproductoFK",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "cotizacionproductoFK" }),
               resolve: context =>
               {
                   var cotizacionid = context.GetArgument<int>("cotizacionproductoFK");
                   return repository.GetAllCotizacionProductoByFk(cotizacionid);
               }
           );
            // Field<ListGraphType<CotizacionProductoType>>(
            //    "cotizacionproductoFK",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
            //    { Name = "cotizacionproductoFK" }),
            //    resolve: context =>
            //    {
            //        var cotizacionid = context.GetArgument<int>("cotizacionproductoFK");
            //        return repository.GetAllCotizacionProductosByCotizacionID(cotizacionid);
            //    }
            //);

            #endregion

           

            #region UsuarioDireccion
            Field<ListGraphType<UsuarioDireccionType>>(
                "usuarioDireccion",
                resolve: context => repository.GetAllUsuarioDireccion());

            Field<ListGraphType<UsuarioDireccionType>>(
               "usuarioDireccionUsuarioID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "usuarioDireccionUsuarioID" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("usuarioDireccionUsuarioID");
                   return repository.GetAllUsuarioDireccionByUsuario(usuario);
               }
           );

            Field<UsuarioDireccionType>(
               "usuarioDireccionID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "usuarioDireccionID" }),
               resolve: context =>
               {
                   var usuariodireccionid = context.GetArgument<int>("usuarioDireccionID");
                   return repository.GetAllUsuarioDireccionById(usuariodireccionid);
               }
           );

            Field<UsuarioIngresoType>(
               "usuarioIngresoID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "usuarioIngresoID" }),
               resolve: context =>
               {
                   var usuarioIngresoID = context.GetArgument<int>("usuarioIngresoID");
                   return repository.GetAllUsuarioIngresoById(usuarioIngresoID);
               }
           );

            Field<ListGraphType<UsuarioIngresoType>>(
                "usuarioIngreso",
                resolve: context => repository.GetAllUsuarioIngreso());
            #endregion


            Field<ListGraphType<ParametroType>>(
                "parametros",
                resolve: context => repository.GetAllParametros());

            #region Pedido
            Field<ListGraphType<PedidoType>>(
                "pedidos",
                resolve: context => repository.GetAllPedidos());

            Field<ListGraphType<PedidoType>>(
               "pedidosID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "pedidosID" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("pedidosID");
                   return repository.GetAllPedidosByPedido(usuario);
               }
           );

            Field<ListGraphType<PedidoType>>(
               "pedidosolicitudID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "pedidosolicitudID" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("pedidosolicitudID");
                   return repository.GetAllPedidosBySolicitud(usuario);
               }
           );
            Field<ListGraphType<PedidoProductoType>>(
               "pedidoproductopedidoID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "pedidoproductopedidoID" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("pedidoproductopedidoID");
                   return repository.GetAllPedidoProductoByPedido(usuario);
               }
           );
            #endregion

            Field<ListGraphType<PedidoLineaTiempoType>>(
               "pedidoLineaTiemposID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "pedidoLineaTiemposID" }),
               resolve: context =>
               {
                   var pedidoID = context.GetArgument<int>("pedidoLineaTiemposID");
                   return repository.GetAllPedidoLineaTiempoByPedido(pedidoID);
               }
           );


            Field<ListGraphType<NotificacionType>>(
               "notificacionusuarioID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
               { Name = "notificacionusuarioID" }),
               resolve: context =>
               {
                   var usuario = context.GetArgument<int>("notificacionusuarioID");
                   return repository.GetAllNotificacionByUsuario(usuario);
               }
           );

            Field<ListGraphType<DistritoType>>(
                "distritos",
                resolve: context => repository.GetAllDistritos());
        }
    }


}

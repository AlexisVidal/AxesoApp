using Axeso_SL.Interfaces;
using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class AxesoMutation : ObjectGraphType
    {
        public AxesoMutation(IRepository usarioRepository)
        {
            //Name = "CreateUsuarioMutation";

            Field<UsuarioType>(
                "createUsuario",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UsuarioInputType>> { Name = "usuario" }
                ),
                resolve: context =>
                {
                    var usuario = context.GetArgument<Usuarios>("usuario");
                    return usarioRepository.Add(usuario);
                });

            Field<UsuarioType>(
               "updatetokenUsuario",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<UsuarioInputType>> { Name = "usuario" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "usuarioID" }
               ),
               resolve: context =>
               {
                   var usuario = context.GetArgument<Usuarios>("usuario");
                   var usuarioID = context.GetArgument<int>("usuarioID");
                   var dbusuario = usarioRepository.GetUsuarioById(usuarioID);
                   return usarioRepository.UpdateToken(dbusuario,usuario);
               });

            #region Solicitud
            Field<SolicitudType>(
                "createSolicitud",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SolicitudInputType>> { Name = "solicitud" }
                ),
                resolve: context =>
                {
                    var solicitud = context.GetArgument<Solicitud>("solicitud");
                    return usarioRepository.AddSolicitud(solicitud);
                });
            #endregion
            #region SolicitudProducto
            Field<SolicitudProductoType>(
                "createSolicitudProducto",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SolicitudProductoInputType>> { Name = "solicitudproducto" }
                ),
                resolve: context =>
                {
                    var solicitudproducto = context.GetArgument<SolicitudProducto>("solicitudproducto");
                    return usarioRepository.AddSolicitudProducto(solicitudproducto);
                });
            #endregion

            #region SolicitudProducto
            Field<SolicitudDataFarmaciasType>(
                "createSolicitudDataFarmacias",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SolicitudDataFarmaciasInputType>> { Name = "solicituddatafarmacias" }
                ),
                resolve: context =>
                {
                    var solicituddatafarmacias = context.GetArgument<SolicitudDataFarmacias>("solicituddatafarmacias");
                    return usarioRepository.AddSolicitudDataFarmacias(solicituddatafarmacias);
                });

            #endregion

            #region UsuarioDireccion
            Field<UsuarioDireccionType>(
                "createUsuarioDireccion",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UsuarioDireccionInputType>> { Name = "usuarioDireccion" }
                ),
                resolve: context =>
                {
                    var usuarioDireccion = context.GetArgument<UsuarioDireccion>("usuarioDireccion");
                    return usarioRepository.AddUsuarioDireccion(usuarioDireccion);
                });

            Field<UsuarioDireccionType>(
               "updateUsuarioDireccion",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<UsuarioDireccionUpInputType>> { Name = "usuariodirnew" }
               ),
               resolve: context =>
               {
                   var usuariodirnew = context.GetArgument<UsuarioDireccion>("usuariodirnew");
                   var dbusuariodirec = usarioRepository.GetAllUsuarioDireccionById(usuariodirnew.UsuarioDireccionID);
                   return usarioRepository.UpdateUsuarioDireccion(dbusuariodirec, usuariodirnew);
               });
            Field<CotizacionType>(
               "updateCotizacion",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<CotizacionUpInputType>> { Name = "cotizacion" }
               ),
               resolve: context =>
               {
                   var cotizacion = context.GetArgument<Cotizacion>("cotizacion");
                   //var dbusuariodirec = usarioRepository.GetAllUsuarioDireccionById(usuariodirnew.UsuarioDireccionID);
                   return usarioRepository.Updatecotizacion(cotizacion);
               });
            Field<UsuarioIngresoType>(
                "createUsuarioIngreso",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UsuarioIngresoInputType>> { Name = "usuarioIngreso" }
                ),
                resolve: context =>
                {
                    var usuarioIngreso = context.GetArgument<UsuarioIngreso>("usuarioIngreso");
                    return usarioRepository.AddUsuarioIngreso(usuarioIngreso);
                });
            Field<UsuarioIngresoType>(
               "updateUsuarioIngreso",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<UsuarioIngresoUpInputType>> { Name = "usuarioIngresonew" }
               ),
               resolve: context =>
               {
                   var usuarioIngresonew = context.GetArgument<UsuarioIngreso>("usuarioIngresonew");
                   var dbusuarioingreso = usarioRepository.GetAllUsuarioIngresoById(usuarioIngresonew.UsuarioID);
                   return usarioRepository.UpdateUsuarioIngreso(dbusuarioingreso, usuarioIngresonew);
               });

            #endregion
            #region Pedidos
            Field<PedidoType>(
                "createPedido",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PedidoInputType>> { Name = "pedido" }
                ),
                resolve: context =>
                {
                    var pedido = context.GetArgument<Pedido>("pedido");
                    return usarioRepository.AddPedido(pedido);
                });
            Field<PedidoProductoType>(
                "createPedidoProducto",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PedidoProductoInputType>> { Name = "pedidoProducto" }
                ),
                resolve: context =>
                {
                    var pedidoProducto = context.GetArgument<PedidoProducto>("pedidoProducto");
                    return usarioRepository.AddPedidoProducto(pedidoProducto);
                });
            #endregion
        }
    }
}

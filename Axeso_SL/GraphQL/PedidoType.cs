using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axeso_BE;
using GraphQL.Types;

namespace Axeso_SL.GraphQL
{
    public class PedidoType : ObjectGraphType<Pedido>
    {
        public PedidoType()
        {

            Field(a => a.PedidoID);
            Field(a => a.Numero);
            Field(a => a.CotizacionID);
            Field(a => a.UsuarioID);
            Field(a => a.SolicitudID);
            Field(a => a.Activo);
            Field(a => a.Fecha);
            Field(a => a.FechaGenerado);
            Field(a => a.Nombre);
            Field(a => a.Direccion);
            Field(a => a.Latitud);
            Field(a => a.Longitud);
            Field(a => a.Estado);
            Field(a => a.NombreEntrega);
            Field(a => a.DireccionEntrega);
            Field(a => a.LatitudEntrega);
            Field(a => a.LongitudEntrega);
            Field(a => a.TotalPagar);
            Field(a => a.TipoPagar);
            Field(a => a.MontoPagar);
            Field(a => a.UsuarioClienteID);
        }
    }
}

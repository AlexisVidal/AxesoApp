using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axeso_BE;
using GraphQL.Types;

namespace Axeso_SL.GraphQL
{
    public class PedidoLineaTiempoType : ObjectGraphType<PedidoLineaTiempo>
    {
        public PedidoLineaTiempoType()
        {
            Field(a => a.PedidoLineaTiempoID);
            Field(a => a.PedidoID);
            Field(a => a.EstadoPedidoID);
            Field(a => a.Descripcion);
            Field(a => a.Fecha);
            Field(a => a.FechaTexto);
            Field(a => a.Numero);
            Field(a => a.UsuarioClienteID);
        }
    }
}

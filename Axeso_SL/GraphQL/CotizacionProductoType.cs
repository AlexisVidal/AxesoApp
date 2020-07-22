using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class CotizacionProductoType : ObjectGraphType<CotizacionProducto>
    {
        public CotizacionProductoType()
        {
            Field(a => a.CotizacionProductoID);
            Field(a => a.CotizacionID);
            Field(a => a.SolicitudProductoID);
            Field(a => a.PrecioUnitario);
            Field(a => a.PrecioTotal);
            Field(a => a.Activo);
            Field(a => a.Cantidad);
            Field<SolicitudProductoType>(nameof(CotizacionProducto.SolicitudProducto));
            Field<CotizacionType>(nameof(CotizacionProducto.Cotizacion));
            Field<SolicitudType>(nameof(CotizacionProducto.Cotizacion.Solicitud));
            Field<UsuarioType>(nameof(CotizacionProducto.Cotizacion.Usuarios));
        }
    }
}

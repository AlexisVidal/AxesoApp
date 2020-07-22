using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class SolicitudType : ObjectGraphType<Solicitud>
    {
        public SolicitudType()
        {
            
            Field(a => a.SolicitudID);
            Field(a => a.SolicitudCode);
            Field(a => a.UsuarioID);
            Field(a => a.Direccion);
            Field(a => a.Latitud);
            Field(a => a.Longitud);
            Field(a => a.Distancia);
            Field(a => a.Fecha);
            Field(a => a.Activo);
            Field(a => a.FechaEnviado);
            Field(a => a.Cotizado);
            Field<UsuarioType>(nameof(Solicitud.Usuarios));

        }
    }
}

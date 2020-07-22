using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class SolicitudDataFarmaciasType : ObjectGraphType<SolicitudDataFarmacias>
    {
        public SolicitudDataFarmaciasType()
        {
            Field(a => a.SolicitudDataFarmaciasID);
            Field(a => a.SolicitudID);
            Field(a => a.UsuarioID);
            Field(a => a.Activo);
            Field<SolicitudType>(nameof(SolicitudDataFarmacias.Solicitud));
            Field<UsuarioType>(nameof(SolicitudDataFarmacias.Usuarios));

        }
    }
}

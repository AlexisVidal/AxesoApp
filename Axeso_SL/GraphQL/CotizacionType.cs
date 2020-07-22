﻿using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class CotizacionType : ObjectGraphType<Cotizacion>
    {
        public CotizacionType()
        {

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
            Field<SolicitudType>(nameof(Cotizacion.Solicitud));
            Field<UsuarioType>(nameof(Cotizacion.Usuarios));
        }
    }
}

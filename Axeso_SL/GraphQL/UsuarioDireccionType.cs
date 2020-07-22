using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class UsuarioDireccionType : ObjectGraphType<UsuarioDireccion>
    {
        public UsuarioDireccionType()
        {
            Field(a => a.UsuarioDireccionID);
            Field(a => a.UsuarioID);
            Field(a => a.Nombre);
            Field(a => a.Direccion);
            Field(a => a.Latitud);
            Field(a => a.Longitud);
            Field(a => a.Activo);
            Field<UsuarioType>(nameof(UsuarioDireccion.Usuarios));
        }
    }
}

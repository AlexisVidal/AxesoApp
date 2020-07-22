using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class UsuarioType : ObjectGraphType<Usuarios>
    {
        public UsuarioType()
        {
            Field(a => a.UsuarioID);
            Field(a => a.TipoUsuarioID);
            Field(a => a.Usuario);
            Field(a => a.Descripcion);
            Field(a => a.Email);
            Field(a => a.Telefono);
            Field(a => a.Activo);
            Field(a => a.Bloqueado);
            Field(a => a.Token);
            Field(a => a.FchHraCreacion);
            Field(a => a.FchHraActualizacion);
            Field(a => a.FchHraBloqueo);
            Field(a => a.FchHraDesbloqueo);
            Field(a => a.DataFarmaciasID);
            Field(a => a.FechaNacimiento);
        }
    }
}

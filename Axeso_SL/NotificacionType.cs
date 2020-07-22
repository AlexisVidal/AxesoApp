using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axeso_BE;
using GraphQL.Types;

namespace Axeso_SL
{
    public class NotificacionType : ObjectGraphType<Notificacion>
    {
        public NotificacionType()
        {

            Field(a => a.NotificacionID);
            Field(a => a.UsuarioID);
            Field(a => a.Hora);
            Field(a => a.Fecha);
            Field(a => a.Descripcion);
            Field(a => a.Accion);
        }
    }
}

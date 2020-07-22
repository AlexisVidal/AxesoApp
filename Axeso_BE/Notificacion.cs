using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class Notificacion
    {
        public int NotificacionID { get; set; }
        public int UsuarioID { get; set; }
        public string Hora { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Accion { get; set; }
    }
}

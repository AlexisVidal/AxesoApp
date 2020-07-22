using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class UsuarioInput 
        //: InputObjectGraphType
    {
        public int UsuarioID { get; set; }
        public string TipoUsuarioID { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }

        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
        public bool Bloqueado { get; set; }
        public string Token { get; set; }
        public DateTime FchHraCreacion { get; set; }
        public DateTime FchHraActualizacion { get; set; }
        public DateTime FchHraBloqueo { get; set; }
        public DateTime FchHraDesbloqueo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int DataFarmaciasID { get; set; }

        //public UsuarioInput()
        //{
        //    Name = "usuarioInput";
        //    Field<NonNullGraphType<IntGraphType>>("usuarioID");
        //    Field<StringGraphType>("tipoUsuarioID");
        //    Field<StringGraphType>("usuario");
        //    Field<StringGraphType>("descripcion");
        //    Field<StringGraphType>("email");
        //    Field<StringGraphType>("telefono");
        //    Field<BooleanGraphType>("activo");
        //    Field<BooleanGraphType>("bloqueado");
        //    Field<StringGraphType>("token");
        //    Field<DateTimeGraphType>("fchHraCreacion");
        //    Field<DateTimeGraphType>("fchHraActualizacion");
        //    Field<DateTimeGraphType>("fchHraBloqueo");
        //    Field<DateTimeGraphType>("fchHraDesbloqueo");
        //}
    }
}

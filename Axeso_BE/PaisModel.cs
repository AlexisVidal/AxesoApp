
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class PaisModel
    {
        public int ID { get; set; }
        public string PaisID { get; set; }
        public string Nombre { get; set; }
        public string CulturaID { get; set; }
        public string MonedaID { get; set; }
        public Cultura Cultura { get; set; }
        public Moneda Moneda { get; set; }

        public List<UbigeoModel> ubigeo { get; set; }
        public List<TipoDocumentoIdentidadModel> tipoDocumentoIdentidad { get; set; }
        //public Pais()
        //{

        //}
        //public Pais(string paisID, string nombre, string culturaID, string monedaID)
        //{
        //    PaisID = paisID;
        //    Nombre = nombre;
        //    CulturaID = culturaID;
        //    MonedaID = monedaID;
        //}
    }
}

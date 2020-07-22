using System.Collections.Generic;

namespace Axeso_BE
{
    public class Moneda
    {
        public int ID { get; set; }
        public string MonedaID { get; set; }
        public string Nombre { get; set; }
        public string Simbolo { get; set; }

        public List<PaisModel> ubigeo { get; set; }

        //public Moneda()
        //{

        //}
        //public Moneda(string monedaID, string nombre, string simbolo)
        //{
        //    MonedaID = monedaID;
        //    Nombre = nombre;
        //    Simbolo = simbolo;
        //}
    }
}

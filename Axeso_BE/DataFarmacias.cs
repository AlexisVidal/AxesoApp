using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class DataFarmacias
    {
        [Key]
        public int DataFarmaciasID { get; set; }
        public string Ruc { get; set; }
        public string Razon_social { get; set; }
        public string Direccion { get; set; }
        public string PaisID { get; set; }
        public int TipoUbigeoID { get; set; }
        public string UbigeoID { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}

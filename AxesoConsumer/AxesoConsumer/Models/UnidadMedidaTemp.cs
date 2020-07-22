using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Models
{
    public class UnidadMedidaTemp
    {
        public int UnidadID { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return string.Format("{0}",
                Nombre);
        }
    }
}

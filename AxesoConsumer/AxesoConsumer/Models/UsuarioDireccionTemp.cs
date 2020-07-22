using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Models
{
    public class UsuarioDireccionTemp
    {
        public int UsuarioDireccionID { get; set; }
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public bool Activo { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", Direccion);
        }
    }
}

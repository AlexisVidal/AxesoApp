using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Models
{
    public class Establecimiento
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Direccion { get; set; }
        public int Rate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

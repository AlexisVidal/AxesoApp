using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Models
{
    public class SolicitudDataFarmaciasLite
    {
        [PrimaryKey, AutoIncrement]
        public int SolicitudDataFarmaciasLiteID { get; set; }
        public string IdSolicitud { get; set; }
        public int UsuarioID { get; set; }
        public bool Activo { get; set; }
    }
}

using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Models
{
    public class ProductoLite
    {
        [PrimaryKey, AutoIncrement]
        public int ProductoLiteID { get; set; }
        public string IdSolicitud { get; set; }
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public int TipoNegocioID { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public int UnidadID_com { get; set; }
        public string UnidadNombre { get; set; }
        public Nullable<int> UnidadID_gra { get; set; }

        public int UnidadId { get; set; }
        public string  UnidadNombreSelect { get; set; }
        public int CategoriaID { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaAbreviatura { get; set; }
        public int Cantidad { get; set; }
        public bool Activo { get; set; }
        public string Imagen { get; set; }

        public bool RequiereReceta { get; set; }

        public int ProductoMarcaID { get; set; }

        public decimal PrecioRef_com { get; set; }
        public decimal PrecioRef_gra { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }

        public string CantPresenta()
        {
            return string.Format("{0} {1}",
                Cantidad.ToString(), UnidadNombre);
        }
    }
}

using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class SolicitudProductoType : ObjectGraphType<SolicitudProducto>
    {
        public SolicitudProductoType()
        {
            Field(a => a.SolicitudProductoID);
            Field(a => a.SolicitudID);
            Field(a => a.ProductoLiteID);
            Field(a => a.ProductoID);
            Field(a => a.TipoNegocioID);
            Field(a => a.Nombre);
            Field(a => a.Abreviatura);
            Field(a => a.UnidadID);
            Field(a => a.UnidadNombre);
            Field(a => a.CategoriaID);
            Field(a => a.CategoriaNombre);
            Field(a => a.CategoriaAbreviatura);
            Field(a => a.Cantidad);
            Field(a => a.Activo);
            Field(a => a.Imagen);
            Field(a => a.PrecioUnitario);
            Field(a => a.PrecioTotal);
            Field(a => a.RequiereReceta);
            Field<SolicitudType>(nameof(SolicitudProducto.Solicitud));
        }
    }
}

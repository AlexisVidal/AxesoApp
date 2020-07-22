using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class ProductoMarcaType : ObjectGraphType<ProductoMarca>
    {
        public ProductoMarcaType()
        {
            Field(a => a.ProductoMarcaID);
            Field(a => a.Nombre);
            Field(a => a.Imagen);
            Field(a => a.Activo);
        }
    }
}

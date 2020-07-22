using Axeso_BE;
using Axeso_SL.Interfaces;
using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class UnidadMedidaType : ObjectGraphType<UnidadMedida>
    {
        public UnidadMedidaType(IRepository repository, IDataLoaderContextAccessor dataLoader)
        {
            Field(a => a.ID);
            Field(a => a.UnidadID);
            Field(a => a.Nombre);
            Field(a => a.Abreviatura);
            Field(a => a.EsUndiadComercial);
            Field(a => a.EsUnidadGranel);
            Field(a => a.Activo);
            }
    }
}

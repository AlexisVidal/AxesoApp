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
    public class TipoProductoType : ObjectGraphType<TipoProducto>
    {
        public TipoProductoType(IRepository repository, IDataLoaderContextAccessor dataLoader)
        {
            Field(a => a.TipoProductoID);
            Field(a => a.Nombre);
            Field(a => a.Abreviatura);
            Field(a => a.Orden);
            Field(a => a.Activo);
            Field<ListGraphType<TipoProductoTipoNegocioType>>(
                "tipoProductoTipoNegocio",
                resolve: context =>
                {
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<int, TipoProductoTipoNegocio>("GetTipoProductoTipoNegocioByTipoProductoIDs", repository.GetTipoProductoTipoNegocioByTipoProductoIDs);
                    return loader.LoadAsync(context.Source.TipoProductoID);
                });
        }
    }
}

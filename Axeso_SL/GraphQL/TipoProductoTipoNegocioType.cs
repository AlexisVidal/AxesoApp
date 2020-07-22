using Axeso_BE;
using Axeso_SL.Interfaces;
using Axeso_SL.Repositories;
using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class TipoProductoTipoNegocioType : ObjectGraphType<TipoProductoTipoNegocio>
    {
        public TipoProductoTipoNegocioType(IRepository repository, IDataLoaderContextAccessor dataLoader)
        {
            Field(x => x.TipoProductoID);
            Field(x => x.TipoNegocioID);
            Field<ListGraphType<TipoProductoType>>(
            "tiposproductos",
            resolve: context =>
            {
                var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<int, TipoProducto>("GetTipoProductosByTipoProductoIds", repository.GetTipoProductosByTipoProductoIds);
                return loader.LoadAsync(context.Source.TipoProductoID);
            });
        }
    }
}
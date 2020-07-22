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
    public class CategoriaType : ObjectGraphType<Categoria>
    {
        public CategoriaType(IRepository repository, IDataLoaderContextAccessor dataLoader)
        {
            Field(a => a.ID);
            Field(a => a.TipoNegocioID);
            Field(a => a.CategoriaID).Name("CategoriaID");
            Field(a => a.CategoriaID_pad, nullable:true, type: typeof(IntGraphType));
            Field(a => a.Nombre);
            Field(a => a.Abreviatura);
            Field(a => a.Orden);
            Field(a => a.Activo);
            Field(a => a.Imagen);
            //Field<ListGraphType<CategoriaType>>(
            //"categorias",
            //resolve: context =>
            //{
            //    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<int, Categoria>("GetCategoriaIds", repository.GetCategoriaIds);
            //    return loader.LoadAsync(context.Source.CategoriaID);
            //});
            //Field<ListGraphType<TipoNegocioType>>(
            //"tipoNegocios",
            //resolve: context =>
            //{
            //    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<int, TipoNegocio>("GetTipoNegocioIds", repository.GetTipoNegocioIds);
            //    return loader.LoadAsync(context.Source.TipoNegocioID);
            //});
        }
    }
}

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
    public class ProductoType: ObjectGraphType<Producto>
    {
        public ProductoType(
            /*IRepository repository, IDataLoaderContextAccessor dataLoader*/)
        {
            Field(a => a.ID);
            Field(a => a.ProductoID);
            Field(a => a.TipoNegocioID);
            Field(a => a.Nombre);
            Field(a => a.Abreviatura);
            //Field(a => a.CategoriaID, nullable: false, type: typeof(IntGraphType));
            Field(a => a.UnidadID_com, nullable: false, type: typeof(IntGraphType));
            Field(a => a.RequiereReceta);
            Field(a => a.Imagen);
            Field(a => a.ProductoMarcaID);
            Field(a => a.Busqueda);
            Field(a => a.UnidadID_gra, nullable: true, type: typeof(IntGraphType));
            Field(a => a.PrecioRef_com);
            Field(a => a.PrecioRef_gra);
            Field<CategoriaType>(nameof(Producto.Categoria));
            Field<UnidadMedidaType>(nameof(Producto.UnidadMedida));
            //Field<ListGraphType<CategoriaType>>(
            //"categorias",
            //resolve: context =>
            //{
            //    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<int, Categoria>("GetCategoriaIds", repository.GetCategoriaIds);
            //    return loader.LoadAsync(context.Source.CategoriaID);
            //});

            //Field<ListGraphType<UnidadMedidaType>>(
            //"unidadMedidas",
            //resolve: context =>
            //{
            //    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<int, UnidadMedida>("GetUnidadMedidaIds", repository.GetUnidadMedidaIds);
            //    return loader.LoadAsync(context.Source.UnidadID_com);
            //});
        }
    }
}

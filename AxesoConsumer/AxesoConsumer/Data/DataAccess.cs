using Axeso_BE;
using AxesoConsumer.Helpers;
using AxesoConsumer.Interfaces;
using AxesoConsumer.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AxesoConsumer.Data
{
    public class DataAccess 
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });
        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DataAccess()
        {
            InitializeAsync().SafeFireAndForget(false);
        }
        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(SolicitudLite).Name) )
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(SolicitudLite)).ConfigureAwait(false);
                    initialized = true;
                }
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ProductoLite).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ProductoLite)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        #region SolicitudLite
        //public Task<int> InsertSolicitud(SolicitudLite solicitud)
        //{
        //    return Database.InsertAsync(solicitud);
        //}
        //public Task<int> UpdateSolicitud(SolicitudLite solicitud)
        //{
        //    return Database.UpdateAsync(solicitud);
        //}
        //public Task<int> DeleteSolicitud(SolicitudLite solicitud)
        //{
        //    return Database.DeleteAsync(solicitud);
        //}
        //public Task<SolicitudLite> GetSolicitud(int IdSolicitud)
        //{
        //    return Database.Table<SolicitudLite>().FirstOrDefaultAsync(c => c.IdSolicitud == IdSolicitud);
        //}
        //public Task<List<SolicitudLite>> GetSolicitudes(string Usuario)
        //{
        //    return Database.Table<SolicitudLite>().Where(c => c.Usuario.ToLower().Equals(Usuario.ToLower())).ToListAsync();
        //}
        #endregion
        #region ProductoLite
        //public Task<int> InsertProductoLite(ProductoLite producto)
        //{
        //    return Database.InsertAsync(producto);
        //}
        //public Task<int> UpdateProductoLite(ProductoLite producto)
        //{
        //    return Database.UpdateAsync(producto);
        //}
        //public Task<List<ProductoLite>> GetProductoLiteByFk(int IdSolicitud)
        //{
            
        //    return Database.Table<ProductoLite>().Where(c => c.IdSolicitud == IdSolicitud).ToListAsync();
        //}
        //public Task<ProductoLite> GetProductoLiteByFkId(int IdSolicitud, int ProductoID)
        //{
        //    return Database.Table<ProductoLite>().FirstOrDefaultAsync(c => c.IdSolicitud == IdSolicitud && c.ProductoID == ProductoID);
        //}
        //public Task<int> DeleteProducto(ProductoLite producto)
        //{
        //    //var result =  Database.QueryAsync<ProductoLite>("delete from ProductoLite where ProductoLiteID = ?", producto.ProductoLiteID).Id;
        //    //return Task.FromResult(result);
        //    try
        //    {
        //        return Database.Table<ProductoLite>().Where(x => x.ProductoLiteID == producto.ProductoLiteID).DeleteAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.FromResult(1);
        //    }
        //}
        #endregion

    }
}

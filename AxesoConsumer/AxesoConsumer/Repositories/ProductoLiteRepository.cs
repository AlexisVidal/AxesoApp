using AxesoConsumer.Dependencies;
using AxesoConsumer.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AxesoConsumer.Repositories
{
    public class ProductoLiteRepository
    {
        private SQLiteConnection cn;

        public ProductoLiteRepository()
        {
            this.cn = DependencyService.Get<IDataBase>().GetConnection();
        }
        public void CrearBBDD()
        {
            if (!this.cn.TableMappings.Any(m => m.MappedType.Name == typeof(ProductoLite).Name))
            {
                this.cn.CreateTable<ProductoLite>();
            }
            //this.cn.DropTable<ProductoLite>();

            this.cn.CreateTable<ProductoLite>();

        }

        public List<ProductoLite> GetProductoLite()
        {
            var consulta = from datos in cn.Table<ProductoLite>()
                           select datos;
            return consulta.ToList();
        }
        public List<ProductoLite> GetProductoLiteBySolicitud(string num)
        {
            var consulta = from datos in cn.Table<ProductoLite>()
                           where datos.IdSolicitud.ToLower().Equals(num.ToLower())
                           select datos;
            return consulta.ToList();
        }
        public ProductoLite BuscarProductoLite(int num)
        {
            var consulta = from datos in cn.Table<ProductoLite>()
                           where datos.ProductoLiteID == num
                           select datos;
            return consulta.FirstOrDefault();
        }
        public ProductoLite BuscarProductoLiteIdFk(string idsolicitud, int fkproducto)
        {
            var consulta = from datos in cn.Table<ProductoLite>()
                           where datos.IdSolicitud.ToLower().Equals(idsolicitud.ToLower()) && datos.ProductoID == fkproducto
                           select datos;
            return consulta.FirstOrDefault();
        }
        public void InsertarProductoLite(ProductoLite product)
        {
            this.cn.Insert(product);
        }

        public void ModificarProductoLite(int idproductolite, int cant, bool activo, decimal dPrecioUnitario, decimal dPrecioTotal)
        {
            ProductoLite prod = this.BuscarProductoLite(idproductolite);
            prod.Cantidad = cant;
            prod.Activo = activo;
            prod.PrecioUnitario = dPrecioUnitario;
            prod.PrecioTotal = dPrecioTotal;
            this.cn.Update(prod);
        }
        public void EliminarProductoLite(int idproductolite)
        {
            ProductoLite prod = this.BuscarProductoLite(idproductolite);
            this.cn.Delete<ProductoLite>(idproductolite);
        }
        public void EliminarListProductoLite(string num)
        {
            var prods = this.GetProductoLiteBySolicitud(num);
            foreach (var item in prods)
            {
                this.cn.Delete<ProductoLite>(item.ProductoLiteID);
            }
            
        }
    }
}

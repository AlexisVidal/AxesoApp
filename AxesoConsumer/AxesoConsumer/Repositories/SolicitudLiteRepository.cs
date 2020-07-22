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
    public class SolicitudLiteRepository
    {
        private SQLiteConnection cn;

        public SolicitudLiteRepository()
        {
            this.cn = DependencyService.Get<IDataBase>().GetConnection();
        }
        public void CrearBBDD()
        {
            if (!this.cn.TableMappings.Any(m => m.MappedType.Name == typeof(SolicitudLite).Name))
            {
                this.cn.CreateTable<SolicitudLite>();
            }
            this.cn.CreateTable<SolicitudLite>();
            //this.cn.DropTable<SolicitudLite>();
        }

        public List<SolicitudLite> GetSolicitudLite()
        {
            var consulta = from datos in cn.Table<SolicitudLite>()
                           select datos;
            return consulta.ToList();
        }
        public List<SolicitudLite> BuscarSolicitudLitesByUser(string nombre)
        {
            var consulta = from datos in cn.Table<SolicitudLite>()
                           where datos.Usuario.ToLower().Equals(nombre.ToLower()) && datos.Enviado == false
                           select datos;
            return consulta.ToList();
        }
        public SolicitudLite BuscarSolicitudLiteByUser(string nombre)
        {
            var consulta = from datos in cn.Table<SolicitudLite>()
                           where datos.Usuario.ToLower().Equals(nombre.ToLower()) && datos.Enviado == false
                           select datos;
            return consulta.FirstOrDefault();
        }
        public SolicitudLite BuscarSolicitudLite(string num)
        {
            var consulta = from datos in cn.Table<SolicitudLite>()
                           where datos.IdSolicitud.ToLower().Equals(num.ToLower())
                           select datos;
            return consulta.FirstOrDefault();
        }
        public SolicitudLite BuscarSolicitudLiteIdFk(string idsolicitud)
        {
            var consulta = from datos in cn.Table<SolicitudLite>()
                           where datos.IdSolicitud.ToLower().Equals(idsolicitud.ToLower())
                           select datos;
            return consulta.FirstOrDefault();
        }
        public SolicitudLite InsertarSolicitudLite(SolicitudLite product)
        {
            this.cn.Insert(product);
            var insertado = this.BuscarSolicitudLiteByUser(product.Usuario);
            return insertado;
        }

        public void ModificarSolicitudLite(string idsolicitudlite, bool activo)
        {
            SolicitudLite prod = this.BuscarSolicitudLite(idsolicitudlite);
            prod.Activo = activo;
            this.cn.Update(prod);
        }
        public void EnviarSolicitudLite(string idsolicitudlite, bool envio)
        {
            SolicitudLite prod = this.BuscarSolicitudLite(idsolicitudlite);
            prod.Enviado = envio;
            this.cn.Update(prod);
        }
        public void EliminarSolicitudLite(string idsolicitudlite)
        {
            SolicitudLite prod = this.BuscarSolicitudLite(idsolicitudlite);
            this.cn.Delete<SolicitudLite>(idsolicitudlite);
        }
    }
}

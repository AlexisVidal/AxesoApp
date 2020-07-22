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
    public class SolicitudDataFarmaciasLiteRepository
    {
        private SQLiteConnection cn;

        public SolicitudDataFarmaciasLiteRepository()
        {
            this.cn = DependencyService.Get<IDataBase>().GetConnection();
        }
        public void CrearBBDD()
        {
            if (!this.cn.TableMappings.Any(m => m.MappedType.Name == typeof(SolicitudDataFarmaciasLite).Name))
            {
                this.cn.CreateTable<SolicitudDataFarmaciasLite>();
            }
            this.cn.CreateTable<SolicitudDataFarmaciasLite>();
            //this.cn.DropTable<SolicitudDataFarmaciasLite>();
        }

        public List<SolicitudDataFarmaciasLite> GetSolicitudDataFarmaciasLite()
        {
            var consulta = from datos in cn.Table<SolicitudDataFarmaciasLite>()
                           select datos;
            return consulta.ToList();
        }
        public List<SolicitudDataFarmaciasLite> BuscarSolicitudDataFarmaciasLitesByFk(string IdSolicitud)
        {
            var consulta = from datos in cn.Table<SolicitudDataFarmaciasLite>()
                           where datos.IdSolicitud.ToLower().Equals(IdSolicitud.ToLower())
                           select datos;
            return consulta.ToList();
        }
        public SolicitudDataFarmaciasLite BuscarSolicitudDataFarmaciasLite(int num)
        {
            var consulta = from datos in cn.Table<SolicitudDataFarmaciasLite>()
                           where datos.SolicitudDataFarmaciasLiteID == num
                           select datos;
            return consulta.FirstOrDefault();
        }
        public void InsertarSolicitudDataFarmaciasLite(SolicitudDataFarmaciasLite entidad)
        {
            this.cn.Insert(entidad);
            //var insertado = this.BuscarSolicitudDataFarmaciasLiteByUser(product.Usuario);
            //return insertado;
        }

        public void EliminarSolicitudDataFarmaciasLite(int idsolicituddatafarmacia)
        {
            //SolicitudDataFarmaciasLite prod = this.BuscarSolicitudDataFarmaciasLite(idsolicituddatafarmacia);
            this.cn.Delete<SolicitudDataFarmaciasLite>(idsolicituddatafarmacia);
        }

        public void EliminarAllSolicitudDataFarmaciasLite(List<SolicitudDataFarmaciasLite> lsoliditudes)
        {
            foreach (var item in lsoliditudes)
            {
                EliminarSolicitudDataFarmaciasLite(item.SolicitudDataFarmaciasLiteID);
            }
        }
    }
}

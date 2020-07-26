using Axeso_BE;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL
{
    public partial class GraphQLDemoContext : DbContext
    {
        public GraphQLDemoContext()
        {
        }

        public GraphQLDemoContext(DbContextOptions<GraphQLDemoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TipoProductoTipoNegocio>().HasKey(ba => new { ba.TipoProductoID, ba.TipoNegocioID });
            modelBuilder.Entity<Categoria>().HasKey(p=>p.CategoriaID);
            modelBuilder.Entity<Distrito>().HasKey(p=>p.DistritoID);
            modelBuilder.Entity<PedidoLineaTiempo>().HasKey(p=>p.PedidoLineaTiempoID);
            modelBuilder.Entity<Producto>().HasKey(ba => new { ba.ProductoID, ba.TipoNegocioID });
            modelBuilder.Entity<Producto>().HasOne(p => p.Categoria).WithMany(p => p.Producto).HasForeignKey(p => p.CategoriaID);
            modelBuilder.Entity<Producto>().HasOne(p => p.UnidadMedida).WithMany(b => b.Producto).HasForeignKey(p => p.UnidadID_com);
            
            modelBuilder.Entity<Solicitud>().HasKey(x => x.SolicitudID);
            modelBuilder.Entity<Solicitud>().HasOne(x => x.Usuarios).WithMany(x => x.Solicitud).HasForeignKey(p => p.UsuarioID);
            modelBuilder.Entity<Solicitud>().HasMany(x => x.SolicitudProducto).WithOne(x => x.Solicitud).HasForeignKey(p => p.SolicitudProductoID);

            modelBuilder.Entity<SolicitudProducto>().HasKey(x => x.SolicitudProductoID);
            modelBuilder.Entity<SolicitudProducto>().HasOne(x => x.Solicitud).WithMany(x => x.SolicitudProducto).HasForeignKey(p => p.SolicitudID);
            modelBuilder.Entity<DataFarmacias>().HasKey(x => x.DataFarmaciasID);
            modelBuilder.Entity<SolicitudDataFarmacias>().HasKey(x => x.SolicitudDataFarmaciasID);
            modelBuilder.Entity<SolicitudDataFarmacias>().HasOne(x => x.Solicitud).WithMany(x => x.SolicitudDataFarmacias).HasForeignKey(p => p.SolicitudID);
            modelBuilder.Entity<SolicitudDataFarmacias>().HasOne(x => x.Usuarios).WithMany(x => x.SolicitudDataFarmacias).HasForeignKey(p => p.UsuarioID);

            modelBuilder.Entity<Usuarios>().HasKey(x => x.UsuarioID);
            modelBuilder.Entity<Pedido>().HasKey(x => x.PedidoID);
            modelBuilder.Entity<PedidoProducto>().HasKey(x => x.PedidoProductoID);

            modelBuilder.Entity<Cotizacion>().HasKey(x => x.CotizacionID);
            modelBuilder.Entity<Cotizacion>().HasOne(x => x.Usuarios).WithMany(x => x.Cotizacion).HasForeignKey(p => p.UsuarioID);
            modelBuilder.Entity<Parametro>().HasKey(x => x.ParametroID);

            modelBuilder.Entity<CotizacionProducto>().HasKey(x => x.CotizacionProductoID);
            modelBuilder.Entity<CotizacionProducto>().HasOne(x => x.Cotizacion).WithMany(x => x.CotizacionProducto).HasForeignKey(p => p.CotizacionID);
            modelBuilder.Entity<CotizacionProducto>().HasOne(x => x.SolicitudProducto).WithMany(x => x.CotizacionProducto).HasForeignKey(p => p.SolicitudProductoID);

            modelBuilder.Entity<UsuarioDireccion>().HasKey(x => x.UsuarioDireccionID);
            modelBuilder.Entity<UsuarioDireccion>().HasOne(x => x.Usuarios).WithMany(x => x.UsuarioDireccion).HasForeignKey(p => p.UsuarioID);
            modelBuilder.Entity<UsuarioDireccion>().HasOne(x => x.Distrito).WithMany(x => x.UsuarioDireccion).HasForeignKey(p => p.DistritoID);
            modelBuilder.Entity<UsuarioIngreso>().HasKey(x => x.UsuarioIngresoID);
            modelBuilder.Entity<ProductoMarca>().HasKey(x => x.ProductoMarcaID);
            modelBuilder.Entity<Notificacion>().HasKey(x => x.NotificacionID);

            //modelBuilder.Entity<Producto>().HasOne(p => p.UnidadMedida).WithMany(b => b.Producto).HasForeignKey(p => p.UnidadID_gra);
            //modelBuilder.Entity<UnidadMedida>().HasMany(unidadMedida => unidadMedida.Producto)
            //               .WithRequired().HasForeignKey(con => con.uni);

            //modelBuilder.Entity<City>().HasMany(city => city.Connections)
            //                           .WithRequired().HasForeignKey(con => con.StartCityId);
        }

        public virtual DbSet<Cultura> Cultura { get; set; }
        public virtual DbSet<Distrito> Distrito { get; set; }
        public virtual DbSet<PedidoLineaTiempo> PedidoLineaTiempo { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PedidoProducto> PedidoProducto { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<TipoProductoTipoNegocio> TipoProductoTipoNegocio { get; set; }
        public virtual DbSet<TipoProducto> TipoProducto { get; set; }
        public virtual DbSet<TipoNegocio> TipoNegocio { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<SolicitudProducto> SolicitudProducto { get; set; }
        public virtual DbSet<DataFarmacias> DataFarmacias { get; set; }
        public virtual DbSet<SolicitudDataFarmacias> SolicitudDataFarmacias { get; set; }
        public virtual DbSet<Cotizacion> Cotizacion { get; set; }
        public virtual DbSet<CotizacionProducto> CotizacionProducto { get; set; }
        public virtual DbSet<UsuarioDireccion> UsuarioDireccion { get; set; }
        public virtual DbSet<ProductoMarca> ProductoMarca { get; set; }
        public virtual DbSet<UsuarioIngreso> UsuarioIngreso { get; set; }
        public virtual DbSet<Parametro> Parametro { get; set; }
        public virtual DbSet<Notificacion> Notificacion { get; set; }
    }

}

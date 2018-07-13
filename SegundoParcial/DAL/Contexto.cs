using SegundoParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Vehiculo> vehiculos { get; set; }
        public DbSet<Taller> talleres { get; set; }
        public DbSet<Articulos> articulos { get; set; }
        public DbSet<EntradaArticulos> entradas { get; set; }
        public DbSet<MantenimientoDetalle> mantenimientos { get; set; }
        public DbSet<Mantenimiento> mantenimiento { get; set; }

        public Contexto() : base("ConStr") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

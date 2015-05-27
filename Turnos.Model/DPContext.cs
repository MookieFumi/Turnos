using System.Data.Entity;
using Turnos.Model.Entities;

namespace Turnos.Model
{
    public class DPContext : DbContext
    {
        public DPContext()
            : base("DPTurnos")
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioTurnoSemanal> UsuariosTurnosSemanales { get; set; }
        public DbSet<UsuarioTurnoSemanalSecuencia> UsuariosTurnosSemanalesSecuencias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empresa>().ToTable("Empresas");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            
            modelBuilder.Entity<UsuarioTurnoSemanal>().ToTable("UsuariosTurnosSemanales");
            modelBuilder.Entity<UsuarioTurnoSemanalSecuencia>().ToTable("UsuariosTurnosSemanalesSecuencias");
            modelBuilder.Entity<UsuarioTurnoSemanalSecuenciaDia>().ToTable("UsuariosTurnosSemanalesSecuenciasDias")
                .HasKey(p => new { p.UsuarioTurnoSemanalSecuenciaId, p.Dia });
        }
    }
}


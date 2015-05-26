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
        public DbSet<UsuarioSecuencia> UsuarioSecuencias { get; set; }
        public DbSet<UsuarioTurno> UsuarioTurnos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empresa>().ToTable("Empresas");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<UsuarioSecuencia>().ToTable("UsuariosSecuencias");
            modelBuilder.Entity<UsuarioTurno>().ToTable("UsuariosTurnos");
            modelBuilder.Entity<UsuarioTurnoDia>()
                .ToTable("UsuariosTurnosDias")
                .HasKey(p => new { p.UsuarioTurnoId, p.Dia });
        }
    }
}


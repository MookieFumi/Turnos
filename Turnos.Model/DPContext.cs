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
        public DbSet<UsuarioSecuencia> UsuariosSecuencias { get; set; }
        public DbSet<UsuarioSecuenciaTurno> UsuariosSecuenciasTurnos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empresa>().ToTable("Empresas");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<UsuarioSecuencia>().ToTable("UsuariosSecuencias");
            modelBuilder.Entity<UsuarioSecuenciaTurno>().ToTable("UsuariosSecuenciasTurnos");
            modelBuilder.Entity<UsuarioSecuenciaTurnoDia>()
                .ToTable("UsuariosSecuenciasTurnosDias")
                .HasKey(p => new { p.UsuarioSecuenciaTurnoId, p.Dia });
        }
    }
}


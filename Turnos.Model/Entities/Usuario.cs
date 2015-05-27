using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;

namespace Turnos.Model.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            TurnosSemanales = new HashSet<UsuarioTurnoSemanal>();
        }

        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public int UsuarioId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<UsuarioTurnoSemanal> TurnosSemanales { get; set; }
    }
}
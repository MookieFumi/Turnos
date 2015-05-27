using System;
using System.Collections.Generic;

namespace Turnos.Model.Entities
{
    public class UsuarioTurnoSemanal
    {
        public UsuarioTurnoSemanal()
        {
            
        }

        public UsuarioTurnoSemanal(DateTime fechaDesde)
        {
            FechaDesde = fechaDesde;
            Secuencias = new HashSet<UsuarioTurnoSemanalSecuencia>();
        }

        public int UsuarioTurnoSemanalId { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public DateTime FechaDesde { get; set; }

        public ICollection<UsuarioTurnoSemanalSecuencia> Secuencias { get; set; }

        public override string ToString()
        {
            return String.Format("{0}",  FechaDesde.ToShortDateString());
        }
    }
}
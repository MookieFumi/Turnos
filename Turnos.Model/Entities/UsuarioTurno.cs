using System;
using System.Collections.Generic;

namespace Turnos.Model.Entities
{
    public class UsuarioTurno
    {
        public UsuarioTurno(DateTime fechaDesde, string nombre, int numeroSemana)
        {
            FechaDesde = fechaDesde;
            Nombre = nombre;
            NumeroSemana = numeroSemana;
            Dias = new HashSet<UsuarioTurnoDia>();
        }

        public int UsuarioTurnoId { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public DateTime FechaDesde { get; set; }

        public int NumeroSemana { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<UsuarioTurnoDia> Dias { get; set; }

        public override string ToString()
        {
            return String.Format("{0}. {1} - {2}", FechaDesde.ToShortDateString(), Nombre, NumeroSemana);
        }
    }
}
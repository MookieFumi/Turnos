using System;
using System.Collections.Generic;

namespace Turnos.Model.Entities
{
    public class UsuarioSecuencia
    {
        public UsuarioSecuencia()
        {
            
        }

        public UsuarioSecuencia(DateTime fechaDesde, string nombre)
        {
            FechaDesde = fechaDesde;
            Nombre = nombre;
            Turnos = new HashSet<UsuarioTurno>();
        }

        public int UsuarioSecuenciaId { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public DateTime FechaDesde { get; set; }
        public string Nombre { get; set; }

        public ICollection<UsuarioTurno> Turnos { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Nombre, FechaDesde.ToShortDateString());
        }
    }
}
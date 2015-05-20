using System;
using System.Collections.Generic;
using System.Linq;
using Turnos.Model;
using Turnos.Model.Entities;

namespace Turnos.Test.DTO
{
    internal class TurnoDeUsuarioDTO
    {
        public int EmpresaId { get; set; }
        public string Nombre { get; set; }
        public ICollection<TurnoDTO> Turnos { get; set; }

        internal class TurnoDTO
        {
            public DateTime FechaDesde { get; set; }
            public int NumeroSemana { get; set; }
            public string Nombre { get; set; }

            public ICollection<DiaDTO> Dias { get; set; }

            public override string ToString()
            {
                return String.Format("{0}. {1} - {2}", FechaDesde.ToShortDateString(), Nombre, NumeroSemana);
            }
        }

        internal class DiaDTO
        {
            public DiaSemana PrimerDiaSemana { get; set; }
            public int Dia { get; set; }
            public Turno Turno { get; set; }
            public bool Trabaja { get; set; }

            public override string ToString()
            {
                IEnumerable<KeyValuePair<int, string>> diasSemana = Utilities.GetDiasSemanaOrdenados((int)PrimerDiaSemana);
                return String.Format("Dia {0} ({1}) Turno: {2}. Trabaja: {3}.", Dia, diasSemana.ElementAt(Dia - 1), Turno.ToString(), Trabaja);
            }
        }
    }
}
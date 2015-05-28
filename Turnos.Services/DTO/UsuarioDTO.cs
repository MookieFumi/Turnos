using System;
using System.Collections.Generic;
using System.Linq;
using Turnos.Model;
using Turnos.Model.Entities;

namespace Turnos.Services.DTO
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public ICollection<TurnoSemanalDTO> TurnosSemanales { get; set; }

        public class TurnoSemanalDTO
        {
            public TurnoSemanalDTO()
            {
                Secuencias = new HashSet<TurnoSemanalSecuenciaDTO>();
            }

            public int UsuarioTurnoSemanalId { get; set; }
            public DateTime FechaDesde { get; set; }
            public ICollection<TurnoSemanalSecuenciaDTO> Secuencias { get; set; }


            public override string ToString()
            {
                return String.Format("{0}", FechaDesde.ToShortDateString());
            }
        }

        public class TurnoSemanalSecuenciaDTO
        {
            public TurnoSemanalSecuenciaDTO()
            {
                Dias = new HashSet<TurnoSemanalSecuenciaDiaDTO>();
            }

            public int UsuarioTurnoSemanalSecuenciaId { get; set; }
            public int Orden { get; set; }
            public ICollection<TurnoSemanalSecuenciaDiaDTO> Dias { get; set; }

            public override string ToString()
            {
                return String.Format("{0}", Orden);
            }
        }

        public class TurnoSemanalSecuenciaDiaDTO
        {
            public int UsuarioTurnoSemanalSecuenciaId { get; set; }

            public int Dia { get; set; }
            public DiaSemana PrimerDiaSemana { get; set; }
            public Turno? Turno { get; set; }

            public override string ToString()
            {
                IEnumerable<KeyValuePair<int, string>> diasSemana = Utilities.GetDiasSemanaOrdenados(PrimerDiaSemana);
                return String.Format("Dia {0} ({1}) Turno: {2}.", Dia, diasSemana.ElementAt(Dia - 1), Turno);
            }
        }
    }
}
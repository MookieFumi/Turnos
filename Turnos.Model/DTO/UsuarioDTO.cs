using System;
using System.Collections.Generic;
using System.Linq;
using Turnos.Model.Entities;

namespace Turnos.Model.DTO
{
    public class UsuarioDTO
    {
        public int EmpresaId { get; set; }
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public ICollection<SecuenciaDTO> Secuencias { get; set; }

        public class SecuenciaDTO
        {
            public SecuenciaDTO()
            {
                Turnos = new HashSet<TurnoDTO>();
            }
            public int UsuarioSecuenciaId { get; set; }
            public DateTime FechaDesde { get; set; }
            public string Nombre { get; set; }
            public ICollection<TurnoDTO> Turnos { get; set; }

            public override string ToString()
            {
                return String.Format("{0} - {1}", Nombre, FechaDesde.ToShortDateString());
            }
        }

        public class TurnoDTO
        {
            public TurnoDTO()
            {
            Dias= new HashSet<TurnoDiaDTO>();    
            }

            public int Orden { get; set; }
            public ICollection<TurnoDiaDTO> Dias { get; set; }

            public override string ToString()
            {
                return String.Format("{0}", Orden);
            }
        }

        public class TurnoDiaDTO
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
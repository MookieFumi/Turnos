using System;
using System.Collections.Generic;
using System.Linq;

namespace Turnos.Model.Entities
{
    public class UsuarioTurnoDia
    {
        public UsuarioTurnoDia(int dia, Turno turno, bool trabaja)
        {
            Dia = dia;
            Turno = turno;
            Trabaja = trabaja;
        }

        public int UsuarioTurnoId { get; set; }
        public virtual UsuarioTurno UsuarioTurno { get; set; }

        public int Dia { get; set; }
        public Turno Turno { get; set; }
        public bool Trabaja { get; set; }

        public override string ToString()
        {
            var primerDiaSemana = (int)UsuarioTurno.UsuarioSecuencia.Usuario.Empresa.PrimerDiaSemana;

            var diasSemana = Utilities.GetDiasSemanaOrdenados(primerDiaSemana);
            return String.Format("Dia {0} ({1}) Turno: {2}. Trabaja: {3}.", Dia, diasSemana.ElementAt(Dia - 1), Turno.ToString(), Trabaja);
        }
    }
}
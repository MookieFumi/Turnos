using System;
using System.Collections.Generic;
using System.Linq;

namespace Turnos.Model.Entities
{
    public class UsuarioTurnoSemanalSecuenciaDia
    {
        public UsuarioTurnoSemanalSecuenciaDia(int dia, Turno turno)
        {
            Dia = dia;
            Turno = turno;
        }

        public virtual int UsuarioTurnoSemanalSecuenciaId { get; set; }
        public virtual UsuarioTurnoSemanalSecuencia UsuarioTurnoSemanalSecuencia { get; set; }

        public int Dia { get; set; }
        public Turno? Turno { get; set; }

        public override string ToString()
        {
            var primerDiaSemana = UsuarioTurnoSemanalSecuencia.UsuarioTurnoSemanal.Usuario.Empresa.PrimerDiaSemana;

            var diasSemana = Utilities.GetDiasSemanaOrdenados(primerDiaSemana);
            return String.Format("Dia {0} ({1}) Turno: {2}.", Dia, diasSemana.ElementAt(Dia - 1), Turno.ToString());
        }
    }
}
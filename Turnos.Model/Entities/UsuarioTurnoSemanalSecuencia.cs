using System;
using System.Collections.Generic;

namespace Turnos.Model.Entities
{
    public class UsuarioTurnoSemanalSecuencia
    {
        public UsuarioTurnoSemanalSecuencia()
        {

        }

        public UsuarioTurnoSemanalSecuencia(int orden)
        {
            Orden = orden;
            Dias = new HashSet<UsuarioTurnoSemanalSecuenciaDia>();
        }

        public int UsuarioTurnoSemanalSecuenciaId { get; set; }

        public int UsuarioTurnoSemanalId { get; set; }
        public virtual UsuarioTurnoSemanal UsuarioTurnoSemanal { get; set; }

        public int Orden { get; set; }

        public virtual ICollection<UsuarioTurnoSemanalSecuenciaDia> Dias { get; set; }

        public override string ToString()
        {
            return String.Format("{0}", Orden);
        }
    }
}
using System;
using System.Collections.Generic;

namespace Turnos.Model.Entities
{
    public class UsuarioTurno
    {
        public UsuarioTurno( int orden)
        {
            Orden = orden;
            Dias = new HashSet<UsuarioTurnoDia>();
        }

        public int UsuarioTurnoId { get; set; }

        public int UsuarioSecuenciaId { get; set; }
        public virtual UsuarioSecuencia UsuarioSecuencia { get; set; }

        public int Orden { get; set; }

        public virtual ICollection<UsuarioTurnoDia> Dias { get; set; }

        public override string ToString()
        {
            return String.Format("{0}", Orden);
        }
    }
}
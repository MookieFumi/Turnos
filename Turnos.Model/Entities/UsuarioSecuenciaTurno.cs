using System;
using System.Collections.Generic;

namespace Turnos.Model.Entities
{
    public class UsuarioSecuenciaTurno
    {
        public UsuarioSecuenciaTurno()
        {

        }

        public UsuarioSecuenciaTurno(int orden)
        {
            Orden = orden;
            Dias = new HashSet<UsuarioSecuenciaTurnoDia>();
        }

        public int UsuarioSecuenciaTurnoId { get; set; }

        public int UsuarioSecuenciaId { get; set; }
        public virtual UsuarioSecuencia UsuarioSecuencia { get; set; }

        public int Orden { get; set; }

        public virtual ICollection<UsuarioSecuenciaTurnoDia> Dias { get; set; }

        public override string ToString()
        {
            return String.Format("{0}", Orden);
        }
    }
}
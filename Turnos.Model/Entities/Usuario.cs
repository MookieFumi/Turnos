using System;
using System.Collections.Generic;
using System.Linq;

namespace Turnos.Model.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            Secuencias = new HashSet<UsuarioSecuencia>();
        }

        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public int UsuarioId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<UsuarioSecuencia> Secuencias { get; set; }

        public void AddTurno(DateTime fechaDesde, string nombre, int orden, Turno turno)
        {
            var secuenciaTurno = new UsuarioSecuencia(fechaDesde, nombre);
            var usuarioTurno = new UsuarioTurno(orden);
            for (var dia = 1; dia <= 7; dia++)
            {
                
                usuarioTurno.Dias.Add(new UsuarioTurnoDia(dia, turno, true));
                secuenciaTurno.Turnos.Add(usuarioTurno);
            }
            
            Secuencias.Add(secuenciaTurno);
        }
    }
}
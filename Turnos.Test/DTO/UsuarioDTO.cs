﻿using System;
using System.Collections.Generic;
using System.Linq;
using Turnos.Model;
using Turnos.Model.Entities;

namespace Turnos.Test.DTO
{
    class UsuarioDTO
    {
        public int EmpresaId { get; set; }
        public string Nombre { get; set; }
        public ICollection<Secuencia> Secuencias { get; set; }

        internal class Secuencia
        {
            public DateTime FechaDesde { get; set; }
            public string Nombre { get; set; }

            public ICollection<Turno> Turnos { get; set; }

            public override string ToString()
            {
                return String.Format("{0} - {1}", Nombre, FechaDesde.ToShortDateString());
            }
        }

        internal class Turno
        {
            public int Orden { get; set; }

            public ICollection<TurnoDia> Dias { get; set; }

            public override string ToString()
            {
                return String.Format("{0}", Orden);
            }
        }

        internal class TurnoDia
        {
            public DiaSemana PrimerDiaSemana { get; set; }
            public int Dia { get; set; }
            public Model.Entities.Turno Turno { get; set; }
            public bool Trabaja { get; set; }

            public override string ToString()
            {
                IEnumerable<KeyValuePair<int, string>> diasSemana = Utilities.GetDiasSemanaOrdenados((int)PrimerDiaSemana);
                return String.Format("Dia {0} ({1}) Turno: {2}. Trabaja: {3}.", Dia, diasSemana.ElementAt(Dia - 1), Turno.ToString(), Trabaja);
            }
        }
    }
}
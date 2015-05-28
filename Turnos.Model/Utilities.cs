using System;
using System.Collections.Generic;
using System.Linq;
using Turnos.Model.Entities;

namespace Turnos.Model
{
    public static class Utilities
    {
        public static IEnumerable<KeyValuePair<int, string>> GetDiasSemanaOrdenados(DiaSemana primerDiaSemana)
        {
            var diasSemana = new Dictionary<int, string>();
            foreach (var value in Enum.GetValues(typeof(DiaSemana)))
            {
                diasSemana.Add((int)value, value.ToString());
            }

            return diasSemana
                .Where(p => p.Key >= (int)primerDiaSemana)
                .Union(diasSemana.Where(p => p.Key < (int)primerDiaSemana));
        }
    }
}

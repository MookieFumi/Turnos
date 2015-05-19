using System;
using System.Collections.Generic;
using System.Linq;
using Turnos.Model.Entities;

namespace Turnos.Model
{
    public class Utilities
    {
        public static IEnumerable<KeyValuePair<int, string>> GetDiasSemanaOrdenados(int primerDiaSemana)
        {
            var diasSemana = new Dictionary<int, string>();
            foreach (var value in Enum.GetValues(typeof(DiaSemana)))
            {
                diasSemana.Add((int)value, value.ToString());
            }

            IEnumerable<KeyValuePair<int, string>> keyValuePairs = diasSemana
                .Where(p => p.Key >= primerDiaSemana)
                .Union(diasSemana.Where(p => p.Key < primerDiaSemana));

            return keyValuePairs;
        }
    }
}

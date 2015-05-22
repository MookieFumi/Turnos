using System;
using Turnos.Model.Entities;

namespace Turnos.Test
{
    public static class TurnoExtensions
    {
        public static Turno GetRandom(this Turno turno, Random random)
        {
            return EnumUtilities<Turno>.GetRandomEnumValue(random);
        }
    }
}
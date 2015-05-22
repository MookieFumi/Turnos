using System;

namespace Turnos.Test
{
    public static class EnumUtilities<T> where T : struct, IConvertible
    {
        public static T GetRandomEnumValue(Random random)
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}
using System;
using EntityFramework.Extensions;
using Turnos.Model;
using Turnos.Model.Entities;

namespace Turnos.Test
{
    public static class DataSeeder
    {
        public static void Seed(DPContext context, Random random)
        {
            context.Empresas.Delete();

            var empresa = new Empresa { PrimerDiaSemana = DiaSemana.Jueves };
            var usuario = new Usuario { Nombre = "Miguel Angel Martín Hrdez", Empresa = empresa };

            var dateTime = DateTime.Now.AddDays(-100);
            for (var i = 1; i <= random.Next(1, 8); i++)
            {
                dateTime = DateTime.Now.AddDays((double)decimal.Divide(-100, i));

                for (var semana = 1; semana <= random.Next(1, 8); semana++)
                {
                    const int daysNumber = 28;
                    //var turno = EnumUtilities<Turno>.GetRandomEnumValue(random);
                    var turno = new Turno().GetRandom(random);
                    usuario.AddTurno(dateTime.AddDays(daysNumber).Date, semana, "Turno Semanal", turno);
                }
            }

            context.Usuarios.Add(usuario);
        }

    }
}
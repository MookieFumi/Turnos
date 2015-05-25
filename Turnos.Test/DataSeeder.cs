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

            var empresa = new Empresa { PrimerDiaSemana = DiaSemana.Miecoles };
            var usuario = new Usuario { Nombre = "Miguel Angel Martín Hrdez", Empresa = empresa };

            var dateTime = DateTime.Now.AddDays(-100);
            var turno = new Turno().GetRandom(random);
            usuario.AddTurno(DateTime.Now.Date, "Turno Semanal", 1, turno);

            context.Usuarios.Add(usuario);
        }
    }
}
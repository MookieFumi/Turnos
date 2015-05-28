using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Turnos.Model;
using Turnos.Model.Entities;
using Turnos.Services;
using Turnos.Services.DTO;

namespace Turnos.Test
{
    public static class DataSeeder
    {
        public static void Seed(DPContext context, Random random)
        {
            context.Empresas.Delete();

            var empresa = new Empresa { PrimerDiaSemana = DiaSemana.Miercoles };
            var usuario = new Usuario { Nombre = "Miguel Angel Martín Hrdez", Empresa = empresa };
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            var fechaDesde = DateTime.Now.AddDays(-20).Date;
            var turnoSemanalDTO = new UsuarioDTO.TurnoSemanalDTO { FechaDesde = fechaDesde };
            turnoSemanalDTO.Secuencias.Add(new UsuarioDTO.TurnoSemanalSecuenciaDTO { Orden = 1, Dias = GetRandomTurnoSemanalSecuenciaDiaDTOItems(random).ToList() });
            turnoSemanalDTO.Secuencias.Add(new UsuarioDTO.TurnoSemanalSecuenciaDTO { Orden = 2, Dias = GetRandomTurnoSemanalSecuenciaDiaDTOItems(random).ToList() });

            var usuariosService = new UsuariosService(context);
            usuariosService.AddTurnoSemanal(usuario.UsuarioId, turnoSemanalDTO);

            context.SaveChanges();
        }

        public static IEnumerable<UsuarioDTO.TurnoSemanalSecuenciaDiaDTO> GetRandomTurnoSemanalSecuenciaDiaDTOItems(Random random)
        {
            var items = Enumerable.Empty<UsuarioDTO.TurnoSemanalSecuenciaDiaDTO>().ToList();
            for (var dia = 1; dia <= 7; dia++)
            {
                var turno = new Turno().GetRandom(random);
                items.Add(new UsuarioDTO.TurnoSemanalSecuenciaDiaDTO
                {
                    Dia = dia,
                    Turno = turno
                });
            }
            return items;
        }
    }
}
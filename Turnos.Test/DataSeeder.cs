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

            var empresa = new Empresa { PrimerDiaSemana = DiaSemana.Miecoles };
            var usuario = new Usuario { Nombre = "Miguel Angel Martín Hrdez", Empresa = empresa };
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            var fechaDesde = DateTime.Now.AddDays(-20).Date;
            var secuenciaDTO = new UsuarioDTO.SecuenciaDTO { FechaDesde = fechaDesde, Nombre = "Secuencia" };
            secuenciaDTO.Turnos.Add(new UsuarioDTO.TurnoDTO { Orden = 1, Dias = GetRandomTurnosDiasDTO(random).ToList() });
            secuenciaDTO.Turnos.Add(new UsuarioDTO.TurnoDTO { Orden = 2, Dias = GetRandomTurnosDiasDTO(random).ToList() });

            var usuariosService = new UsuariosService(context);
            usuariosService.AddSecuenciaTurno(usuario.UsuarioId, secuenciaDTO);

            context.SaveChanges();
        }

        public static IEnumerable<UsuarioDTO.TurnoDiaDTO> GetRandomTurnosDiasDTO(Random random)
        {
            var items = Enumerable.Empty<UsuarioDTO.TurnoDiaDTO>().ToList();
            for (var dia = 1; dia <= 7; dia++)
            {
                var turno = new Turno().GetRandom(random);
                items.Add(new UsuarioDTO.TurnoDiaDTO
                {
                    Dia = dia,
                    Turno = turno,
                    Trabaja = true
                });
            }
            return items;
        }
    }
}
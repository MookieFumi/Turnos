using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignByContract;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using Turnos.Model;
using Turnos.Model.Entities;
using Turnos.Services;
using Turnos.Services.DTO;

namespace Turnos.Test
{
    public class UsuariosServiceTest
    {
        private Stopwatch _stopwatch;
        private Random _random;
        private DPContext _context;
        private IUsuariosService _usuarioService;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _random = new Random();
            _context = new DPContext();
            _usuarioService = new UsuariosService(_context);

            DataSeeder.Seed(_context, _random);
        }

        [SetUp]
        public void SetUp()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void When_LoadData_WithDTO_Data_Is_Loaded()
        {
            var usuario = _usuarioService.GetUsuarioDTO();
            Debug.WriteLine(usuario.Nombre + "\n");
            var secuencias = usuario.TurnosSemanales
                .OrderByDescending(p => p.FechaDesde);

            secuencias.Each(secuencia =>
            {
                Debug.WriteLine(secuencia.ToString());
                secuencia.Secuencias.Each(turno =>
                {
                    Debug.WriteLine("\t" + turno.ToString());
                    turno.Dias.Each(dia => Debug.WriteLine("\t" + dia.ToString()));
                });
                Debug.WriteLine(String.Empty);
            });
        }

        [Test]
        public void When_AddSecuenciaTurno_Insert_Data()
        {
            var fechaDesde = DateTime.Now.AddDays(-10).Date;
            const string nombre = "Secuencia-10";
            var secuenciaDTO = new UsuarioDTO.TurnoSemanalDTO { FechaDesde = fechaDesde };
            secuenciaDTO.Secuencias.Add(new UsuarioDTO.TurnoSemanalSecuenciaDTO { Orden = 1, Dias = DataSeeder.GetRandomTurnoSemanalSecuenciaDiaDTOItems(_random).ToList() });
            secuenciaDTO.Secuencias.Add(new UsuarioDTO.TurnoSemanalSecuenciaDTO { Orden = 2, Dias = DataSeeder.GetRandomTurnoSemanalSecuenciaDiaDTOItems(_random).ToList() });
            secuenciaDTO.Secuencias.Add(new UsuarioDTO.TurnoSemanalSecuenciaDTO { Orden = 3, Dias = DataSeeder.GetRandomTurnoSemanalSecuenciaDiaDTOItems(_random).ToList() });

            var usuario = _usuarioService.GetUsuarioDTO();
            var usuariosService = new UsuariosService(_context);
            usuariosService.AddTurnoSemanal(usuario.UsuarioId, secuenciaDTO);

            _context.SaveChanges();

            Assert.IsTrue(_usuarioService.GetUsuarioDTO().TurnosSemanales.Any(p => p.FechaDesde == fechaDesde));
        }

        [Test]
        public void When_AddSecuenciaTurno_If_SecuenciaDTO_Is_Null_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() =>
            {
                var usuario = _usuarioService.GetUsuarioDTO();
                _usuarioService.AddTurnoSemanal(usuario.UsuarioId, null);
            });
        }

        [Test]
        public void When_AddSecuencia_If_UsuarioId_Not_Exists_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() => _usuarioService.AddTurnoSemanal(9999, new UsuarioDTO.TurnoSemanalDTO()));
        }

        [Test]
        public void When_UpdateSecuenciaTurno_Update_Data()
        {
            var usuarioDTO = _usuarioService.GetUsuarioDTO();
            var secuenciaDTO = usuarioDTO.TurnosSemanales.First();
            secuenciaDTO.Secuencias.ToList().Each(turno =>
                turno.Dias.ToList().Each(dia =>
                    dia.Turno = new Turno().GetRandom(_random)
                )
            );

            _usuarioService.UpdateTurnoSemanal(usuarioDTO.UsuarioId, usuarioDTO.TurnosSemanales.First().UsuarioTurnoSemanalId, secuenciaDTO);
            _context.SaveChanges();

            Assert.IsTrue(_usuarioService.GetUsuarioDTO().TurnosSemanales.Any(p => p.FechaDesde == secuenciaDTO.FechaDesde));
        }

        [Test]
        public void When_UpdateSecuenciaTurno_If_SecuenciaId_Not_Exists_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() =>
            {
                var usuario = _usuarioService.GetUsuarioDTO();
                _usuarioService.UpdateTurnoSemanal(usuario.UsuarioId, 999, new UsuarioDTO.TurnoSemanalDTO());
            });
        }

        [Test]
        public void When_UpdateSecuenciaTurno_If_UsuarioId_Not_Exists_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() => _usuarioService.UpdateTurnoSemanal(9999, 9999, new UsuarioDTO.TurnoSemanalDTO()));
        }

        [Test]
        public void When_UpdateSecuenciaTurno_If_SecuenciaDTO_Is_Null_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() =>
            {
                var usuario = _usuarioService.GetUsuarioDTO();
                _usuarioService.UpdateTurnoSemanal(usuario.UsuarioId, usuario.TurnosSemanales.First().UsuarioTurnoSemanalId, null);
            });
        }

        [Test]
        public void When_RemoveSecuenciaTurno_Remove_Data()
        {
            var usuarioDTO = _usuarioService.GetUsuarioDTO();
            var secuenciaDTO = usuarioDTO.TurnosSemanales.First();

            _usuarioService.RemoveTurnoSemanal(usuarioDTO.UsuarioId, secuenciaDTO.UsuarioTurnoSemanalId);
            _context.SaveChanges();

            Assert.IsTrue(_usuarioService.GetUsuarioDTO().TurnosSemanales.All(p => p.UsuarioTurnoSemanalId != secuenciaDTO.UsuarioTurnoSemanalId));
        }

        [Test]
        public void When_RemoveSecuenciaTurno_If_SecuenciaId_Not_Exists_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() =>
            {
                var usuario = _usuarioService.GetUsuarioDTO();
                _usuarioService.RemoveTurnoSemanal(usuario.UsuarioId, 999);
            });
        }

        [Test]
        public void When_RemoveSecuenciaTurno_If_UsuarioId_Not_Exists_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() => _usuarioService.RemoveTurnoSemanal(9999, 9999));
        }

        [Test]
        [TestCase(DiaSemana.Lunes, DiaSemana.Domingo)]
        [TestCase(DiaSemana.Martes, DiaSemana.Lunes)]
        [TestCase(DiaSemana.Miercoles, DiaSemana.Martes)]
        [TestCase(DiaSemana.Jueves, DiaSemana.Miercoles)]
        [TestCase(DiaSemana.Viernes, DiaSemana.Jueves)]
        [TestCase(DiaSemana.Sabado, DiaSemana.Viernes)]
        [TestCase(DiaSemana.Domingo, DiaSemana.Sabado)]
        public void When_GetDiasSemanaOrdenados_Returns_Expected_Data(DiaSemana primerDiaSemana, DiaSemana ultimaDiaSemana)
        {
            IEnumerable<KeyValuePair<int, string>> list = Utilities.GetDiasSemanaOrdenados(primerDiaSemana).ToList();
            Assert.AreEqual(list.First().Key, (int)primerDiaSemana);
            Assert.AreEqual(list.Last().Key, (int)ultimaDiaSemana);
        }
    }
}

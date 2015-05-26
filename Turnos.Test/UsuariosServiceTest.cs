using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignByContract;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using Turnos.Model;
using Turnos.Model.DTO;
using Turnos.Model.Entities;
using Turnos.Model.Services;

namespace Turnos.Test
{
    public class UsuariosServiceTest
    {
        private Stopwatch _stopwatch;
        private Random _random;
        private DPContext _context;
        private UsuariosService _usuarioService;

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
            var secuencias = usuario.Secuencias
                .OrderByDescending(p => p.FechaDesde);

            secuencias.Each(secuencia =>
            {
                Debug.WriteLine(secuencia.ToString());
                secuencia.Turnos.Each(turno =>
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
            var secuenciaDTO = new UsuarioDTO.SecuenciaDTO { FechaDesde = fechaDesde, Nombre = nombre };
            secuenciaDTO.Turnos.Add(new UsuarioDTO.TurnoDTO { Orden = 1, Dias = DataSeeder.GetRandomTurnosDiasDTO(_random).ToList() });
            secuenciaDTO.Turnos.Add(new UsuarioDTO.TurnoDTO { Orden = 2, Dias = DataSeeder.GetRandomTurnosDiasDTO(_random).ToList() });
            secuenciaDTO.Turnos.Add(new UsuarioDTO.TurnoDTO { Orden = 3, Dias = DataSeeder.GetRandomTurnosDiasDTO(_random).ToList() });

            var usuario = _context.Usuarios.First();
            var usuariosService = new UsuariosService(_context);
            usuariosService.AddSecuenciaTurno(usuario.UsuarioId, secuenciaDTO);

            _context.SaveChanges();

            Assert.IsTrue(usuario.Secuencias.Any(p => p.FechaDesde == fechaDesde && p.Nombre == nombre));
        }

        [Test]
        public void When_AddSecuenciaTurno_If_SecuenciaDTO_Is_Null_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() =>
            {
                var usuario = _context.Usuarios.First();
                _usuarioService.AddSecuenciaTurno(usuario.UsuarioId, null);
            });
        }

        [Test]
        public void When_AddSecuencia_If_UsuarioId_Not_Exists_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() => _usuarioService.AddSecuenciaTurno(9999, new UsuarioDTO.SecuenciaDTO()));
        }

        [Test]
        public void When_UpdateSecuenciaTurno_Update_Data()
        {
            var usuarioDTO = _usuarioService.GetUsuarioDTO();
            var secuenciaDTO = usuarioDTO.Secuencias.First();
            secuenciaDTO.Nombre = "S E C U E N C I A --";
            secuenciaDTO.Turnos.ToList().Each(turno =>
            {
                turno.Dias.ToList().Each(dia =>
                    dia.Turno = new Turno().GetRandom(_random)
                );
            });

            _usuarioService.UpdateSecuenciaTurno(usuarioDTO.UsuarioId, usuarioDTO.Secuencias.First().UsuarioSecuenciaId, secuenciaDTO);
            _context.SaveChanges();

            Assert.IsTrue(_usuarioService.GetUsuarioDTO().Secuencias.Any(p => p.Nombre == secuenciaDTO.Nombre && p.FechaDesde == secuenciaDTO.FechaDesde));
        }

        [Test]
        public void When_UpdateSecuenciaTurno_If_SecuenciaId_Not_Exists_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() =>
            {
                var usuario = _usuarioService.GetUsuarioDTO();
                _usuarioService.UpdateSecuenciaTurno(usuario.UsuarioId, 999, new UsuarioDTO.SecuenciaDTO());
            });
        }

        [Test]
        public void When_UpdateSecuenciaTurno_If_UsuarioId_Not_Exists_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() => _usuarioService.UpdateSecuenciaTurno(9999, 9999, new UsuarioDTO.SecuenciaDTO()));
        }

        [Test]
        public void When_UpdateSecuenciaTurno_If_SecuenciaDTO_Is_Null_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() =>
            {
                var usuario = _usuarioService.GetUsuarioDTO();
                _usuarioService.UpdateSecuenciaTurno(usuario.UsuarioId, usuario.Secuencias.First().UsuarioSecuenciaId, null);
            });
        }

        [Test]
        public void When_RemoveSecuenciaTurno_Remove_Data()
        {
            var usuarioDTO = _usuarioService.GetUsuarioDTO();
            var secuenciaDTO = usuarioDTO.Secuencias.First();

            _usuarioService.RemoveSecuenciaTurno(usuarioDTO.UsuarioId, secuenciaDTO.UsuarioSecuenciaId);
            _context.SaveChanges();

            Assert.IsTrue(!_usuarioService.GetUsuarioDTO().Secuencias.Any(p => p.UsuarioSecuenciaId == secuenciaDTO.UsuarioSecuenciaId));
        }

        [Test]
        public void When_RemoveSecuenciaTurno_If_SecuenciaId_Not_Exists_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() =>
            {
                var usuario = _usuarioService.GetUsuarioDTO();
                _usuarioService.RemoveSecuenciaTurno(usuario.UsuarioId, 999);
            });
        }

        [Test]
        public void When_RemoveSecuenciaTurno_If_UsuarioId_Not_Exists_Throws_DbcException()
        {
            Assert.Throws<DbcException>(() => _usuarioService.RemoveSecuenciaTurno(9999, 9999));
        }
    }
}

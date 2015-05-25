using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using Turnos.Model;
using Turnos.Model.Entities;
using Turnos.Test.DTO;

namespace Turnos.Test
{
    public class GetUsuarioTest
    {
        private Stopwatch _stopwatch;
        private Random _random;
        private DPContext _context;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            SetAutoMapperMaps();

            _context = new DPContext();
            _random = new Random();

            DataSeeder.Seed(_context, _random);
            _context.SaveChanges();
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
        public void LoadData()
        {
            var usuario = GetUsuario();
            Debug.WriteLine(usuario.Nombre + "\n");
            var secuenciaTurnos = from st in usuario.Secuencias
                                  orderby st.FechaDesde descending
                                  select st;
            //var turnos = usuario.Turnos.OrderByDescending(p => p.FechaDesde).ThenBy(p => p.orden);
            foreach (var secuenciaTurno in secuenciaTurnos)
            {
                Debug.WriteLine(secuenciaTurno.ToString());
                foreach (var turno in secuenciaTurno.Turnos)
                {
                    Debug.WriteLine("\t" + turno.ToString());
                    foreach (var dia in turno.Dias)
                    {
                        Debug.WriteLine("\t" + dia.ToString());
                    }
                }
                Debug.WriteLine(String.Empty);
            }
        }

        [Test]
        public void LoadDataWithDTO()
        {
            var usuario = GetUsuarioDTO();
            Debug.WriteLine(usuario.Nombre + "\n");
            var secuencias = usuario.Secuencias
                .OrderByDescending(p => p.FechaDesde);

            foreach (var secuencia in secuencias)
            {
                Debug.WriteLine(secuencia.ToString());
                foreach (var turno in secuencia.Turnos)
                {
                    Debug.WriteLine("\t" + turno.ToString());
                    foreach (var dia in turno.Dias)
                    {
                        Debug.WriteLine("\t" + dia.ToString());
                    }
                }
            }
        }

        private UsuarioDTO GetUsuarioDTO()
        {
            var turnoDeUsuarioDTO = _context.Usuarios
                .Project()
                .To<UsuarioDTO>()
                .First();
            return turnoDeUsuarioDTO;
        }

        private Usuario GetUsuario()
        {
            return _context.Usuarios.First();
        }

        private void SetAutoMapperMaps()
        {
            Mapper.CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dst => dst.Secuencias, opt => opt.MapFrom(src => src.Secuencias));

            Mapper.CreateMap<UsuarioSecuencia, UsuarioDTO.Secuencia>();

            Mapper.CreateMap<UsuarioTurno, UsuarioDTO.Turno>();

            Mapper.CreateMap<UsuarioTurnoDia, UsuarioDTO.TurnoDia>()
                .ForMember(dst => dst.PrimerDiaSemana, opt => opt.MapFrom(src => src.UsuarioTurno.UsuarioSecuencia.Usuario.Empresa.PrimerDiaSemana));
        }
    }
}

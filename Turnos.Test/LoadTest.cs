using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using Turnos.Model;
using Turnos.Model.Entities;
using Turnos.Test.DTO;

namespace Turnos.Test
{
    public class LoadTest
    {
        private Stopwatch _stopwatch;
        private Random _random;
        private DPContext _context;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _random = new Random();
            _context = new DPContext();
            DeleteData(_context);
            SeedData(_context);
            SetAutoMapperMaps();
        }

        [SetUp]
        public void SetUp()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        [Test(Description = "Cargamos del contexto y mediante Lazy Loading carga lo que va necesitando")]
        public void LoadData()
        {
            var usuario = GetUsuario(_context);
            Debug.WriteLine(usuario.Nombre + "\n");
            foreach (var turno in usuario.Turnos.OrderByDescending(p => p.FechaDesde).ThenBy(p => p.NumeroSemana))
            {
                Debug.WriteLine(turno.ToString());
                foreach (var dia in turno.Dias)
                {
                    Debug.WriteLine("\t" + dia.ToString());
                }
                Debug.WriteLine(String.Empty);
            }
        }

        [Test (Description = "Cargamos del contexto mediante una proyección a un DTO")]
        public void LoadDataWithDTO()
        {
            var usuario = GetUsuarioDTO(_context);
            Debug.WriteLine(usuario.Nombre + "\n");
            foreach (var turno in usuario.Turnos.OrderByDescending(p => p.FechaDesde).ThenBy(p => p.NumeroSemana))
            {
                Debug.WriteLine(turno.ToString());
                foreach (var dia in turno.Dias)
                {
                    Debug.WriteLine("\t" + dia.ToString());
                }
                Debug.WriteLine(String.Empty);
            }
        }

        private TurnoDeUsuarioDTO GetUsuarioDTO(DPContext context)
        {
            return context.Usuarios
                    .Project()
                    .To<TurnoDeUsuarioDTO>()
                    .First();
        }

        private Usuario GetUsuario(DPContext context)
        {
            return context.Usuarios.First();
        }

        private void SetAutoMapperMaps()
        {
            Mapper.CreateMap<Usuario, TurnoDeUsuarioDTO>();
            Mapper.CreateMap<UsuarioTurno, TurnoDeUsuarioDTO.TurnoDTO>();
            Mapper.CreateMap<UsuarioTurnoDia, TurnoDeUsuarioDTO.DiaDTO>()
                .ForMember(dst => dst.PrimerDiaSemana,
                    opt => opt.MapFrom(src => src.UsuarioTurno.Usuario.Empresa.PrimerDiaSemana));
        }

        private void SeedData(DPContext context)
        {
            var empresa = new Empresa { PrimerDiaSemana = DiaSemana.Jueves };
            var usuario = new Usuario { Nombre = "Miguel Angel Martín Hrdez", Empresa = empresa };

            var dateTime = DateTime.Now.AddDays(-100);
            for (int i = 1; i <= _random.Next(1, 8); i++)
            {
                dateTime = DateTime.Now.AddDays((double)decimal.Divide(-100, i));

                for (var semana = 1; semana <= _random.Next(1, 8); semana++)
                {
                    const int daysNumber = 28;
                    usuario.AddTurno(dateTime.AddDays(daysNumber).Date, semana, "Turno Semana");
                }
            }

            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        private void DeleteData(DPContext context)
        {
            context.Empresas.Delete();
            context.SaveChanges();
        }
    }
}

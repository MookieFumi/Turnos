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
    public class GetUsuarioTest
    {
        private Stopwatch _stopwatch;
        private Random _random;
        private DPContext _context;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            SetAutoMapperMaps();

            _random = new Random();
            _context = new DPContext();

            DeleteData(_context);
            SeedData(_context);
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
            var turnos = from t in usuario.Turnos
                         orderby t.FechaDesde descending
                         orderby t.NumeroSemana
                         select t;
            //var turnos = usuario.Turnos.OrderByDescending(p => p.FechaDesde).ThenBy(p => p.NumeroSemana);
            foreach (var turno in turnos)
            {
                Debug.WriteLine(turno.ToString());
                foreach (var dia in turno.Dias)
                {
                    Debug.WriteLine("\t" + dia.ToString());
                }
                Debug.WriteLine(String.Empty);
            }
        }

        [Test]
        public void LoadDataWithDTO()
        {
            var usuario = GetUsuarioDTO();
            Debug.WriteLine(usuario.Nombre + "\n");
            var turnos = usuario.Turnos
                .OrderByDescending(p => p.FechaDesde)
                .ThenBy(p => p.NumeroSemana);
            foreach (var turno in turnos)
            {
                Debug.WriteLine(turno.ToString());
                foreach (var dia in turno.Dias)
                {
                    Debug.WriteLine("\t" + dia.ToString());
                }
                Debug.WriteLine(String.Empty);
            }
        }

        private TurnoDeUsuarioDTO GetUsuarioDTO()
        {
            return _context.Usuarios
                    .Project()
                    .To<TurnoDeUsuarioDTO>()
                    .First();
        }

        private Usuario GetUsuario()
        {
            return _context.Usuarios.First();
        }

        private void SetAutoMapperMaps()
        {
            Mapper.CreateMap<Usuario, TurnoDeUsuarioDTO>()
                .ReverseMap();
            Mapper.CreateMap<UsuarioTurno, TurnoDeUsuarioDTO.TurnoDTO>()
                .ReverseMap();
            Mapper.CreateMap<UsuarioTurnoDia, TurnoDeUsuarioDTO.DiaDTO>()
                .ForMember(dst => dst.PrimerDiaSemana, opt => opt.MapFrom(src => src.UsuarioTurno.Usuario.Empresa.PrimerDiaSemana))
                .ReverseMap();
        }

        private void SeedData(DPContext context)
        {
            var empresa = new Empresa { PrimerDiaSemana = DiaSemana.Jueves };
            var usuario = new Usuario { Nombre = "Miguel Angel Martín Hrdez", Empresa = empresa };

            var dateTime = DateTime.Now.AddDays(-100);
            for (var i = 1; i <= _random.Next(1, 8); i++)
            {
                dateTime = DateTime.Now.AddDays((double)decimal.Divide(-100, i));

                for (var semana = 1; semana <= _random.Next(1, 8); semana++)
                {
                    const int daysNumber = 28;
                    var turno = GetRandomEnumValue<Turno>();
                    usuario.AddTurno(dateTime.AddDays(daysNumber).Date, semana, "Turno Semanal", turno);
                }
            }

            context.Usuarios.Add(usuario);
        }

        private void DeleteData(DPContext context)
        {
            context.Empresas.Delete();
        }

        private T GetRandomEnumValue<T>() where T : struct, IConvertible
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(_random.Next(values.Length));
        }
    }
}

using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignByContract;
using Turnos.Model;
using Turnos.Model.Entities;
using Turnos.Services.DTO;

namespace Turnos.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly DPContext _context;

        public UsuariosService(DPContext context)
        {
            _context = context;
            SetAutoMapperMaps();
        }

        #region IUsuariosService members

        public void AddTurnoSemanal(int usuarioId, UsuarioDTO.TurnoSemanalDTO turnoSemanalDTO)
        {
            var usuario = _context.Usuarios.Find(usuarioId);
            Dbc.Requires(usuario != null, "No existe el usuario seleccinado");
            Dbc.Requires(turnoSemanalDTO != null);
            Dbc.Requires(!usuario.TurnosSemanales.Any(p => p.FechaDesde == turnoSemanalDTO.FechaDesde), "Ya existe una secuencia para la fecha introducida");

            var turnoSemanal = new UsuarioTurnoSemanal(turnoSemanalDTO.FechaDesde);
            Mapper.Map(turnoSemanalDTO, turnoSemanal);
            usuario.TurnosSemanales.Add(turnoSemanal);
        }

        public UsuarioDTO GetUsuarioDTO()
        {
            var turnoDeUsuarioDTO = _context.Usuarios
                .Project()
                .To<UsuarioDTO>()
                .First();
            return turnoDeUsuarioDTO;
        }

        public void RemoveTurnoSemanal(int usuarioId, int usuarioTurnoSemanalId)
        {
            var usuario = _context.Usuarios.Find(usuarioId);
            Dbc.Requires(usuario != null, "No existe el usuario seleccinado");
            Dbc.Requires(usuario.TurnosSemanales.Any(p => p.UsuarioTurnoSemanalId == usuarioTurnoSemanalId), "No existe la secuencia seleccionada");

            var turnoSemanal = usuario.TurnosSemanales.Single(p => p.UsuarioTurnoSemanalId == usuarioTurnoSemanalId);
            _context.UsuariosTurnosSemanales.Remove(turnoSemanal);
        }

        public void UpdateTurnoSemanal(int usuarioId, int usuarioTurnoSemanalId, UsuarioDTO.TurnoSemanalDTO turnoSemanalDTO)
        {
            var usuario = _context.Usuarios.Find(usuarioId);
            Dbc.Requires(usuario != null, "No existe el usuario seleccinado");
            Dbc.Requires(usuario.TurnosSemanales.Any(p => p.UsuarioTurnoSemanalId == usuarioTurnoSemanalId), "No existe una secuencia para la fecha seleccionada");
            Dbc.Requires(turnoSemanalDTO != null);

            var turnosSemanalesSecuencias = _context.UsuariosTurnosSemanalesSecuencias.Where(p => p.UsuarioTurnoSemanalId == usuarioTurnoSemanalId).ToList();
            foreach (var turnoSemanalSecuencia in turnosSemanalesSecuencias)
            {
                _context.UsuariosTurnosSemanalesSecuencias.Remove(turnoSemanalSecuencia);
            }

            var turnoSemanal = usuario.TurnosSemanales.Single(p => p.UsuarioTurnoSemanalId == turnoSemanalDTO.UsuarioTurnoSemanalId);
            Mapper.Map(turnoSemanalDTO, turnoSemanal);
        }

        #endregion

        private static void SetAutoMapperMaps()
        {
            Mapper.CreateMap<Usuario, UsuarioDTO>();

            Mapper.CreateMap<UsuarioDTO.TurnoSemanalDTO, UsuarioTurnoSemanal>()
                .ReverseMap();

            Mapper.CreateMap<UsuarioDTO.TurnoSemanalSecuenciaDTO, UsuarioTurnoSemanalSecuencia>()
                .ReverseMap();

            Mapper.CreateMap<UsuarioDTO.TurnoSemanalSecuenciaDiaDTO, UsuarioTurnoSemanalSecuenciaDia>();

            Mapper.CreateMap<UsuarioTurnoSemanalSecuenciaDia, UsuarioDTO.TurnoSemanalSecuenciaDiaDTO>()
                .ForMember(dst => dst.PrimerDiaSemana,
                    opt => opt.MapFrom(src => src.UsuarioTurnoSemanalSecuencia.UsuarioTurnoSemanal.Usuario.Empresa.PrimerDiaSemana));
        }
    }
}

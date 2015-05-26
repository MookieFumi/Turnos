using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignByContract;
using Turnos.Model.DTO;
using Turnos.Model.Entities;

namespace Turnos.Model.Services
{
    public class UsuariosService
    {
        private readonly DPContext _context;

        public UsuariosService(DPContext context)
        {
            _context = context;

            SetAutoMapperMaps();
        }

        public void AddSecuenciaTurno(int usuarioId, UsuarioDTO.SecuenciaDTO secuenciaDTO)
        {
            var usuario = _context.Usuarios.Find(usuarioId);
            Dbc.Requires(usuario != null, "No existe el usuario seleccinado");
            Dbc.Requires(secuenciaDTO != null);
            Dbc.Requires(!usuario.Secuencias.Any(p => p.FechaDesde == secuenciaDTO.FechaDesde), "Ya existe una secuencia para la fecha introducida");

            var usuarioSecuencia = new UsuarioSecuencia(secuenciaDTO.FechaDesde, secuenciaDTO.Nombre);
            Mapper.Map(secuenciaDTO, usuarioSecuencia);
            usuario.Secuencias.Add(usuarioSecuencia);
        }

        public UsuarioDTO GetUsuarioDTO()
        {
            var turnoDeUsuarioDTO = _context.Usuarios
                .Project()
                .To<UsuarioDTO>()
                .First();
            return turnoDeUsuarioDTO;
        }

        public void UpdateSecuenciaTurno(int usuarioId, int usuarioSecuenciaId, UsuarioDTO.SecuenciaDTO secuenciaDTO)
        {
            var usuario = _context.Usuarios.Find(usuarioId);
            Dbc.Requires(usuario != null, "No existe el usuario seleccinado");
            Dbc.Requires(usuario.Secuencias.Any(p => p.UsuarioSecuenciaId == usuarioSecuenciaId), "No existe una secuencia para la fecha seleccionada");
            Dbc.Requires(secuenciaDTO != null);

            var usuarioTurnos = _context.UsuarioTurnos.Where(p => p.UsuarioSecuenciaId == usuarioSecuenciaId).ToList();
            foreach (var usuarioTurno in usuarioTurnos)
            {
                _context.UsuarioTurnos.Remove(usuarioTurno);
            }

            var secuencia = usuario.Secuencias.Single(p => p.UsuarioSecuenciaId == secuenciaDTO.UsuarioSecuenciaId);
            Mapper.Map(secuenciaDTO, secuencia);
        }

        public void RemoveSecuenciaTurno(int usuarioId, int usuarioSecuenciaId)
        {
            var usuario = _context.Usuarios.Find(usuarioId);
            Dbc.Requires(usuario != null, "No existe el usuario seleccinado");
            Dbc.Requires(usuario.Secuencias.Any(p => p.UsuarioSecuenciaId == usuarioSecuenciaId), "No existe la secuencia seleccionada");

            var usuarioSecuencia = usuario.Secuencias.Single(p => p.UsuarioSecuenciaId == usuarioSecuenciaId);
            _context.UsuarioSecuencias.Remove(usuarioSecuencia);
        }

        private static void SetAutoMapperMaps()
        {
            Mapper.CreateMap<Usuario, UsuarioDTO>();

            Mapper.CreateMap<UsuarioDTO.SecuenciaDTO, UsuarioSecuencia>()
                .ReverseMap();

            Mapper.CreateMap<UsuarioDTO.TurnoDTO, UsuarioTurno>()
                .ReverseMap();

            Mapper.CreateMap<UsuarioDTO.TurnoDiaDTO, UsuarioTurnoDia>();

            Mapper.CreateMap<UsuarioTurnoDia, UsuarioDTO.TurnoDiaDTO>()
                .ForMember(dst => dst.PrimerDiaSemana,
                    opt => opt.MapFrom(src => src.UsuarioTurno.UsuarioSecuencia.Usuario.Empresa.PrimerDiaSemana));
        }
    }
}

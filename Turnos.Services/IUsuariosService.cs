using Turnos.Services.DTO;

namespace Turnos.Services
{
    public interface IUsuariosService
    {
        void AddTurnoSemanal(int usuarioId, UsuarioDTO.TurnoSemanalDTO turnoSemanalDTO);
        UsuarioDTO GetUsuarioDTO();
        void RemoveTurnoSemanal(int usuarioId, int usuarioTurnoSemanalId);
        void UpdateTurnoSemanal(int usuarioId, int usuarioTurnoSemanalId, UsuarioDTO.TurnoSemanalDTO turnoSemanalDTO);
    }
}
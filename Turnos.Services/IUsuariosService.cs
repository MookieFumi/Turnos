using Turnos.Services.DTO;

namespace Turnos.Services
{
    public interface IUsuariosService
    {
        void AddSecuenciaTurno(int usuarioId, UsuarioDTO.SecuenciaDTO secuenciaDTO);
        UsuarioDTO GetUsuarioDTO();
        void RemoveSecuenciaTurno(int usuarioId, int usuarioSecuenciaId);
        void UpdateSecuenciaTurno(int usuarioId, int usuarioSecuenciaId, UsuarioDTO.SecuenciaDTO secuenciaDTO);
    }
}
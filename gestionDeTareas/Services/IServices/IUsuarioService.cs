using gestionDeTareas.Dto.UsuariosDto;

namespace gestionDeTareas.Services.IServices
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> ObtenerUsuarios();
        Task<UsuarioDto?> ObtenerPorId(int id);
        Task<UsuarioDto> CrearUsuario(CrearUsuarioDto dto);
        Task<bool> EliminarUsuario(int id);
    }

}

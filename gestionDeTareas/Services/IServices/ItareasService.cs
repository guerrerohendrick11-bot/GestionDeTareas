using gestionDeTareas.DTOs;

namespace gestionDeTareas.Services
{
    public interface ITareaService
    {
        Task<IEnumerable<TareaReadDto>> ObtenerTodasAsync();
        Task<TareaReadDto?> ObtenerPorIdAsync(int id);
        Task<TareaReadDto> CrearAsync(TareaCreateDto dto);
        Task<bool> ActualizarAsync(int id, TareaUpdateDto dto);
        Task<bool> EliminarAsync(int id);
    }
}

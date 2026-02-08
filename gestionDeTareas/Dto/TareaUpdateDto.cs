using gestionDeTareas.Entidades;

namespace gestionDeTareas.DTOs
{
    public class TareaUpdateDto
    {
        public string Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public EstadoTarea Estado { get; set; }

        public DateTime? FechaVencimiento { get; set; }
    }
}

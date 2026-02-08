using gestionDeTareas.Entidades;

namespace gestionDeTareas.DTOs
{
    public class TareaCreateDto
    {
        public string Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public EstadoTarea Estado { get; set; }

        public DateTime? FechaVencimiento { get; set; }
    }
}

namespace gestionDeTareas.Entidades
{
    public class Tarea
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public EstadoTarea Estado { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaVencimiento { get; set; }

        public int? UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}

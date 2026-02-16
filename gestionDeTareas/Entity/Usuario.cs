namespace gestionDeTareas.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public List<Tarea> Tareas { get; set; } = new();
    }
}

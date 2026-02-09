namespace GestionDeTareasBlazor.Models
{
    public class TareasDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
       
        public EstadoTarea Estado { get; set; }
    }
}

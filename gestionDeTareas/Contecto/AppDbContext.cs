using Microsoft.EntityFrameworkCore;
using gestionDeTareas.Entidades;

namespace gestionDeTareas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tarea> Tareas { get; set; }
    }
}

using gestionDeTareas.Data;
using gestionDeTareas.DTOs;
using gestionDeTareas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace gestionDeTareas.Services
{
    public class TareaService : ITareaService
    {
        private readonly AppDbContext _context;

        public TareaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TareaReadDto>> ObtenerTodasAsync()
        {
            return await _context.Tareas
                .Select(t => new TareaReadDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    FechaCreacion = t.FechaCreacion,
                    FechaVencimiento = t.FechaVencimiento
                })
                .ToListAsync();
        }

        public async Task<TareaReadDto?> ObtenerPorIdAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null) return null;

            return new TareaReadDto
            {
                Id = tarea.Id,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                Estado = tarea.Estado,
                FechaCreacion = tarea.FechaCreacion,
                FechaVencimiento = tarea.FechaVencimiento
            };
        }

        public async Task<TareaReadDto> CrearAsync(TareaCreateDto dto)
        {
            var tarea = new Tarea
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                FechaVencimiento = dto.FechaVencimiento,
                FechaCreacion = DateTime.Now
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return new TareaReadDto
            {
                Id = tarea.Id,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                Estado = tarea.Estado,
                FechaCreacion = tarea.FechaCreacion,
                FechaVencimiento = tarea.FechaVencimiento
            };
        }

        public async Task<bool> ActualizarAsync(int id, TareaUpdateDto dto)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return false;

            tarea.Titulo = dto.Titulo;
            tarea.Descripcion = dto.Descripcion;
            tarea.Estado = dto.Estado;
            tarea.FechaVencimiento = dto.FechaVencimiento;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return false;

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

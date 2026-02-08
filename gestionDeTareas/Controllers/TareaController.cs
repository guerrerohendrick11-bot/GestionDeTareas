using gestionDeTareas.DTOs;
using gestionDeTareas.Services;
using Microsoft.AspNetCore.Mvc;

namespace gestionDeTareas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareasController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        // GET: api/tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TareaReadDto>>> GetTareas()
        {
            var tareas = await _tareaService.ObtenerTodasAsync();
            return Ok(tareas);
        }

        // GET: api/tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TareaReadDto>> GetTarea(int id)
        {
            var tarea = await _tareaService.ObtenerPorIdAsync(id);

            if (tarea == null)
                return NotFound();

            return Ok(tarea);
        }

        // POST: api/tareas
        [HttpPost]
        public async Task<ActionResult<TareaReadDto>> CrearTarea(TareaCreateDto dto)
        {
            var tarea = await _tareaService.CrearAsync(dto);

            return CreatedAtAction(
                nameof(GetTarea),
                new { id = tarea.Id },
                tarea
            );
        }

        // PUT: api/tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTarea(int id, TareaUpdateDto dto)
        {
            var actualizado = await _tareaService.ActualizarAsync(id, dto);

            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTarea(int id)
        {
            var eliminado = await _tareaService.EliminarAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}

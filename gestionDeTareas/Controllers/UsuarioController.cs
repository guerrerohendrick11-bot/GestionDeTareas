using gestionDeTareas.Dto.UsuariosDto;
using gestionDeTareas.DTOs;
using gestionDeTareas.Services;
using gestionDeTareas.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace gestionDeTareas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDto>>> Obtener()
        {
            var usuarios = await _service.ObtenerUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> ObtenerPorId(int id)
        {
            var usuario = await _service.ObtenerPorId(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Crear(CrearUsuarioDto dto)
        {
            var nuevoUsuario = await _service.CrearUsuario(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoUsuario.Id }, nuevoUsuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminado = await _service.EliminarUsuario(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}

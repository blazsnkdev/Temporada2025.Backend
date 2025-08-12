using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Temporada2025.Backend.DTOs;
using Temporada2025.Backend.Services;

namespace Temporada2025.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticaController : ControllerBase
    {
        private readonly IEstadisticaService _estadisticaService;
        public EstadisticaController(IEstadisticaService estadisticaService)
        {
            _estadisticaService = estadisticaService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEstadisticaPorId(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }
            var estadistica = await _estadisticaService.ObtenerEstadistica(id);
            if (estadistica == null)
            {
                return NotFound("Estadística no encontrada");
            }
            return Ok(estadistica);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarEstadistica([FromBody] RegistrarEstadisticaRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null");
            }
            bool resultado = await _estadisticaService.RegistrarEstadistica(request);
            if (!resultado)
            {
                return BadRequest("Error al registrar la estadística");
            }
            return Ok("Estadística registrada correctamente");
        }

        [HttpGet]
        public async Task<IActionResult> ListarEstadisticas()
        {
            var listadoEstadisticas = await _estadisticaService.ListarEstadisticas();
            if (listadoEstadisticas == null || !listadoEstadisticas.Any())
            {
                return NotFound("No se encontraron estadísticas");
            }
            return Ok(listadoEstadisticas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEstadistica(Guid id, [FromBody] ActualizarEstadisticaRequest request)
        {
            if (id == Guid.Empty || request == null)
            {
                return BadRequest("Error en el id o el request");
            }
            bool resultado = await _estadisticaService.ActualizarEstadistica(id, request);
            if (!resultado)
            {
                return NotFound("Estadística no encontrada o error al actualizar");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEstadistica(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id invalido");
            }
            bool resultado = await _estadisticaService.EliminarEstadistica(id);
            if (!resultado)
            {
                return NotFound("Estadística no encontrada o error al eliminar");
            }
            return NoContent();
        }



    }
}

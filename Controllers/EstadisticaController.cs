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
            return Ok(await _estadisticaService.ListarEstadisticas());
        }




    }
}

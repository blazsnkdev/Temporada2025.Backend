using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Temporada2025.Backend.DTOs;
using Temporada2025.Backend.Models;
using Temporada2025.Backend.Services;

namespace Temporada2025.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorService _jugadorService;
        public JugadorController(IJugadorService jugadorService)
        {
            _jugadorService = jugadorService;
        }


        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrarJugadorRequest request)
        {
            var result = await _jugadorService.RegistrarJugador(request);

            if(!result.Item1)
                return BadRequest($"Error: {result.Item2}");

            return Ok($"Jugador registrado, Password: {result.Item2}");
        }


    }
}

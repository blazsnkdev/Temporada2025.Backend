using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Temporada2025.Backend.DTOs;
using Temporada2025.Backend.Services;

namespace Temporada2025.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJugadorService _jugadorService;
        public AuthController(IJugadorService jugadorService)
        {
            _jugadorService = jugadorService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var result = await _jugadorService.ValidarJugador(request);
            if (!result.Item1)
                return Unauthorized($"Error: {result.Item2}");
            
            return Ok(new { Token = result.Item2 });
        }


    }
}

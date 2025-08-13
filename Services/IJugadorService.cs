using Temporada2025.Backend.DTOs;
using Temporada2025.Backend.DTOs.Response;
using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Services
{
    public interface IJugadorService
    {
        public string GenerarPassword(string nombre, string apellidoPaterno, DateTime fechaRegistro);
        public bool ValidarPosicion(string posicion);
        public bool ValidarPie(string pie);
        public bool ValidarDorsal(int dorsal);
        Task<DetalleJugadorResponse?> DetalleJugador();
        Task<(bool,string?)> RegistrarJugador(RegistrarJugadorRequest request);
        Task<(bool,string?)> ValidarJugador(LoginDto request);

        Guid? ObtenerJugadorId();
        string GenerarTokenJwt(Jugador jugador);
    }
}

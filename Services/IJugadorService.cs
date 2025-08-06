using Temporada2025.Backend.DTOs;
using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Services
{
    public interface IJugadorService
    {
        public string GenerarPassword(string nombre, string apellidoPaterno, DateTime fechaRegistro);
        public bool ValidarPosicion(string posicion);
        public bool ValidarPie(string pie);
        public bool ValidarDorsal(int dorsal);

        Task<(bool,string?)> RegistrarJugador(RegistrarJugadorRequest request);
        


    }
}

using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Data
{
    public interface IJugadorRepository
    {
        Task<IEnumerable<Jugador>> GetAllAsync();
        Task<Jugador?> GetByIdAsync(Guid id);
        Task AddAsync(Jugador jugador);
        Task UpdateAsync(Jugador jugador);
        Task DeleteAsync(Jugador jugador);
        Task<Jugador?> GetJugadorByNombreAndPassword(string nombre, string password);
        //Task<IEnumerable<Jugador>> GetJugadoresByEquipoIdAsync(Guid equipoId);
        //Task<IEnumerable<Jugador>> GetJugadoresByPosicionAsync(string posicion);
    }
}

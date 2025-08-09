using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Data
{
    public interface IEstadisticaRepository
    {
        Task<IEnumerable<Estadistica>> GetAllByIdJugadorAsync(Guid JugadorId);
        Task AddAsync(Estadistica estadistica);

    }
}

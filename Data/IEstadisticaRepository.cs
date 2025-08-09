using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Data
{
    public interface IEstadisticaRepository
    {
        Task<IEnumerable<Estadistica>> GetAllAsync(Guid id);
        Task AddAsync(Estadistica estadistica);

    }
}

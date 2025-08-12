using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Data
{
    public interface IEstadisticaRepository
    {
        Task<IEnumerable<Estadistica>> GetAllAsync(Guid id);
        Task AddAsync(Estadistica estadistica);
        Task<Estadistica> GetByIdAsync(Guid id);
        Task UpdateAsync(Estadistica estadistica);
        Task DeleteAsync(Estadistica estadistica);

    }
}

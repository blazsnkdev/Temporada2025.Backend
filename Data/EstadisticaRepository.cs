using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Data
{
    public class EstadisticaRepository : Repository<Estadistica>, IEstadisticaRepository
    {
        private readonly AppDbContext _context;
        public EstadisticaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


    }
}

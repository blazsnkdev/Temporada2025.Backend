
namespace Temporada2025.Backend.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IJugadorRepository JugadorRepository { get; }
        public IEstadisticaRepository EstadisticaRepository { get; }
        private readonly AppDbContext _context;

        public UnitOfWork(IJugadorRepository jugadorRepository, AppDbContext context, IEstadisticaRepository estadisticaRepository)
        {
            JugadorRepository = jugadorRepository;
            _context = context;
            EstadisticaRepository = estadisticaRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

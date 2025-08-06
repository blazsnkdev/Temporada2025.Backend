
namespace Temporada2025.Backend.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public JugadorRepository JugadorRepository { get; }
        private readonly AppDbContext _context;

        public UnitOfWork(JugadorRepository jugadorRepository, AppDbContext context)
        {
            JugadorRepository = jugadorRepository;
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

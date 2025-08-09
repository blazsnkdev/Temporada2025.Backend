namespace Temporada2025.Backend.Data
{
    public interface IUnitOfWork
    {
        public IJugadorRepository JugadorRepository { get; }
        public IEstadisticaRepository EstadisticaRepository { get; }
        Task<int> SaveChangesAsync();
    }
}

namespace Temporada2025.Backend.Data
{
    public interface IUnitOfWork
    {
        public IJugadorRepository JugadorRepository { get; }
        Task<int> SaveChangesAsync();
    }
}

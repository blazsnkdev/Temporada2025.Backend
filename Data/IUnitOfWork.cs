namespace Temporada2025.Backend.Data
{
    public interface IUnitOfWork
    {
        public JugadorRepository JugadorRepository { get; }
        Task<int> SaveChangesAsync();
    }
}

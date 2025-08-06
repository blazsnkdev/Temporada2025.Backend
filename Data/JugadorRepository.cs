using Microsoft.EntityFrameworkCore;
using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Data
{
    public class JugadorRepository : Repository<Jugador>, IJugadorRepository
    {
        private readonly AppDbContext _context;
        public JugadorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExisteJugadorAsync(string nombre, string apellidoPaterno, string apellidoMaterno, DateOnly fechaNacimiento)
        {
            return await _context.TblJugador.AnyAsync(j =>
                j.Nombre == nombre &&
                j.ApellidoPaterno == apellidoPaterno &&
                j.ApellidoMaterno == apellidoMaterno &&
                j.FechaNacimiento == fechaNacimiento);
        }


        public async Task<Jugador?> GetJugadorByNombreAndPassword(string nombre, string password)
        {
            return await _context.TblJugador
                .FirstOrDefaultAsync(j => j.Nombre == nombre && j.Password == password);
        }
    }
}

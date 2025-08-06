using Microsoft.EntityFrameworkCore;
using Temporada2025.Backend.Models;

namespace Temporada2025.Backend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Jugador>()
                .HasMany(j => j.Estadisticas)
                .WithOne(e => e.Jugador)
                .HasForeignKey(e => e.JugadorId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Jugador> TblJugador { get; set; }
        public DbSet<Estadistica> TblEstadistica { get; set; }
        

    }
}

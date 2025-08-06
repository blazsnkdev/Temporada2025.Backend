namespace Temporada2025.Backend.Models
{
    public class Estadistica
    {
        public Guid Id { get; set; }
        public DateOnly FechaJornada { get; set; }
        public int PartidosJugados { get; set; }
        public int Goles { get; set; }
        public int Asistencias { get; set; }
        
        public Guid JugadorId { get; set; }
        public Jugador? Jugador { get; set; }
        public double Puntaje { get; set; } //definir el puntaje atraves de una algoritmo en base a gole asistencias y partidos jguados
        public DateTime FechaRegistro { get; set; }
        public Estadistica()
        {
            
        }
    }
}

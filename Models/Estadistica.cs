namespace Temporada2025.Backend.Models
{
    public class Estadistica
    {
        public Guid Id { get; set; }
        public DateOnly FechaJornada { get; set; }
        public int PartidosJugados { get; set; }
        public int Goles { get; set; }
        public int Asistencias { get; set; }
        
        public Guid JugadorId { get; set; }//esta va venir del token de la api
        public Jugador? Jugador { get; set; }

        //definir el puntaje atraves de una algoritmo en base a gole asistencias y partidos jguados
        public double Puntaje { get; set; } 
        
        public DateTime FechaRegistro { get; set; }


        public Estadistica(
            Guid id,
            DateOnly fechaJornada,//siempre va haber
            int partidosJugados, //puede ser 0
            int goles,//puede ser 0
            int asistencias, //puede ser 0
            Guid jugadorId,//puede ser 0
            double puntaje,
            DateTime fechaRegistro)
        {
            Id = id;
            FechaJornada = fechaJornada;
            PartidosJugados = partidosJugados;
            Goles = goles;
            Asistencias = asistencias;
            JugadorId = jugadorId;
            Puntaje = puntaje;
            FechaRegistro = fechaRegistro;
        }
        public Estadistica()
        {
            
        }
    }
}

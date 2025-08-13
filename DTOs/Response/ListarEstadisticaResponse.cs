namespace Temporada2025.Backend.DTOs.Response
{
    public sealed record EstadisticaResponse
 (
    Guid id,
     DateTime fechaJornada,
     int partidosJugados,
     int goles,
     int asistencias,
     double puntaje,
     DateTime fechaRegistro
 );

}

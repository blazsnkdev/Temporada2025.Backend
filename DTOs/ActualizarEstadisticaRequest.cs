namespace Temporada2025.Backend.DTOs
{
    public sealed record ActualizarEstadisticaRequest
    (
        int partidosJugados,
        int goles,
        int asistencias
        );
}

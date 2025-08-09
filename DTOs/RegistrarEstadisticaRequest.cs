namespace Temporada2025.Backend.DTOs
{
    public sealed record RegistrarEstadisticaRequest(
        
        DateOnly fechaJornada,
        int partidosJugados,
        int goles,
        int asistencias
        //el id jugador se obtiene del token de la api
        );
    
}

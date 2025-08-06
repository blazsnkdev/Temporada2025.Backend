namespace Temporada2025.Backend.DTOs
{
    public sealed record RegistrarJugadorRequest(
        string nombre,
        string apellidoPaterno,
        string apellidoMaterno,
        string posicion,
        string pie,
        int dorsal,
        DateOnly fechaNacimiento
        );
    
}

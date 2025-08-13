namespace Temporada2025.Backend.DTOs.Response
{
    public sealed record DetalleJugadorResponse
    (
        string nombre,
        string apellidoPaterno,
        string apellidoMaterno,
        string posicion,
        string pie,
        int dorsal,
        DateOnly fechaNacimiento
        );
}

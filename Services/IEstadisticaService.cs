using Temporada2025.Backend.DTOs;
using Temporada2025.Backend.DTOs.Response;
using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Services
{
    public interface IEstadisticaService
    {

        bool ValidarJornada(int partidosJugados = 0);

        double CalcularPuntaje(int partidosJugados = 0,int goles = 0,int asistencias = 0);//si no vienen valores se establece como 0


        bool ValidarFechaJornada(DateOnly fechaJornada);

        Task<bool> RegistrarEstadistica(RegistrarEstadisticaRequest request);


        Task<List<ListarEstadisticaResponse>> ListarEstadisticas();

    }
}

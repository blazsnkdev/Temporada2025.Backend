using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using Temporada2025.Backend.Data;
using Temporada2025.Backend.DTOs;
using Temporada2025.Backend.DTOs.Response;
using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Services
{
    public class EstadisticaService : IEstadisticaService
    {



        private readonly IUnitOfWork _UoW;
        private readonly IConfiguration _configuration;
        private readonly IJugadorService _jugadorService;

        public EstadisticaService(IUnitOfWork UoW, IConfiguration configuration, IJugadorService jugadorService)
        {

            _UoW = UoW;
            _configuration = configuration;
            _jugadorService = jugadorService;
        }

        public double CalcularPuntaje(int partidosJugados= 0, int goles=0, int asistencias=0)
        {
           
            int valorGoles = goles * 3;
            int valorAsistencias = asistencias * 2;
            return (valorGoles+valorAsistencias)/ partidosJugados;//si o si retonar un numero aun que sea null
        }

        //agregar una funcion para obtener el jugador por id del token

        public async Task<bool> RegistrarEstadistica(RegistrarEstadisticaRequest request)
        {
           
            bool fechaValida = ValidarFechaJornada(request.fechaJornada);
            if (!fechaValida)
                return false;

            var jugadorId = _jugadorService.ObtenerJugadorId();
            if (jugadorId is null)
                return false;

            var yaExiste = await _UoW.EstadisticaRepository.GetByFechaJornada(request.fechaJornada, jugadorId.Value);
            if (yaExiste != null)
            {
                return false;
            }

            double puntaje = CalcularPuntaje(request.partidosJugados, request.goles, request.asistencias);

            var estadistica = new Estadistica() { 
            Id = Guid.NewGuid(),
            FechaJornada = request.fechaJornada,
            PartidosJugados = request.partidosJugados,
            Goles = request.goles,
            Asistencias = request.asistencias,
            JugadorId = jugadorId.Value,
            Puntaje = puntaje,
            FechaRegistro = DateTime.Now
            };
            
            await _UoW.EstadisticaRepository.AddAsync(estadistica);
            return true;

        }

        public bool ValidarFechaJornada(DateOnly fechaJornada)
        {
            var fechaActual = DateOnly.FromDateTime(DateTime.Now);
            return fechaJornada.Year == 2025 && fechaJornada <= fechaActual;

        }

        public bool ValidarJornada(int partidosJugados = 0)//esto para validar si en la jornada se jugo
        {
            if(partidosJugados <= 0)
            {
                return false;//no se jugo
            }
            return true; //se jugo al menos un partido
        }

        public async Task<List<EstadisticaResponse>> ListarEstadisticas()
        {
            var estadisticas = new List<EstadisticaResponse>();

            var jugadorId = _jugadorService.ObtenerJugadorId();

            var connectionString = _configuration.GetConnectionString("cn1");

            using (var connection = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT
                        Id,
                        FechaJornada,
                        PartidosJugados,
                        Goles,
                        Asistencias,
                        Puntaje,
                        FechaRegistro
                    FROM TblEstadistica
                    WHERE JugadorId = @JugadorId 
                    ORDER BY FechaJornada DESC;";

                var result = await connection.QueryAsync<EstadisticaResponse>(
                    sql,
                    new { JugadorId = jugadorId }
                );

                estadisticas = result.ToList();
            }

            return estadisticas;
        }

        public async Task<EstadisticaResponse?> ObtenerEstadistica(Guid estadisticaId)
        {
            var jugadorId = _jugadorService.ObtenerJugadorId();

            var connectionString = _configuration.GetConnectionString("cn1");
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT 
                        Id,
                        FechaJornada,
                        PartidosJugados,
                        Goles,
                        Asistencias,
                        Puntaje,
                        FechaRegistro
                    FROM TblEstadistica
                    WHERE Id = @EstadisticaId
                    AND JugadorId = @JugadorId
                    ORDER BY FechaJornada DESC;";
                var result = await connection.QueryFirstOrDefaultAsync<EstadisticaResponse>(
                    sql,
                    new { EstadisticaId = estadisticaId,JugadorId = jugadorId }
                );
                return result;
            }
        }



        public async Task<bool> ActualizarEstadistica(Guid estadisticaId, ActualizarEstadisticaRequest request)
        {
            var estadistica = await _UoW.EstadisticaRepository.GetByIdAsync(estadisticaId);
            var jugadorId = _jugadorService.ObtenerJugadorId();
            if (estadistica == null)
                return false;
            if(jugadorId is null)
                return false;

            estadistica.PartidosJugados = request.partidosJugados;
            estadistica.Goles = request.goles;
            estadistica.Asistencias = request.asistencias;
            estadistica.Puntaje = CalcularPuntaje(request.partidosJugados, request.goles, request.asistencias);
            estadistica.FechaRegistro = DateTime.Now;
            await _UoW.EstadisticaRepository.UpdateAsync(estadistica);
            return true;
        }

        public async Task<bool> EliminarEstadistica(Guid estadisticaId)
        {
            var estadistica = await _UoW.EstadisticaRepository.GetByIdAsync(estadisticaId);
            var jugadorId = _jugadorService.ObtenerJugadorId();
            if (estadistica == null)
                return false;
            if (jugadorId is null)
                return false;
            await _UoW.EstadisticaRepository.DeleteAsync(estadistica);
            return true;
        }
    }
}

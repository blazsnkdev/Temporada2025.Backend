using System.Security.Claims;
using System.Threading.Tasks;
using Temporada2025.Backend.Data;
using Temporada2025.Backend.DTOs;
using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Services
{
    public class EstadisticaService : IEstadisticaService
    {

        //registrar esto ene l progam
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUnitOfWork _UoW;


        public EstadisticaService(IHttpContextAccessor httpContextAccessor, IUnitOfWork UoW)
        {
            _httpContextAccessor = httpContextAccessor;
            _UoW = UoW;
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

            //aqui obtenemos el id del jugador desde el token de autenticacion
            var jugadorIdClaim = _httpContextAccessor.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(jugadorIdClaim, out var jugadorId))
                return false;

            double puntaje = CalcularPuntaje(request.partidosJugados, request.goles, request.asistencias);

            var estadistica = new Estadistica() { 
            Id = Guid.NewGuid(),
            FechaJornada = request.fechaJornada,
            PartidosJugados = request.partidosJugados,
            Goles = request.goles,
            Asistencias = request.asistencias,
            JugadorId = jugadorId,
            Puntaje = puntaje,
            FechaRegistro = DateTime.Now
            };
            
            await _UoW.EstadisticaRepository.AddAsync(estadistica);
            return true;

        }

        public bool ValidarFechaJornada(DateOnly fechaJornada)
        {
            if(fechaJornada.Year != 2025)
            {
                return false; 
            }
            return true; 
        }

        public bool ValidarJornada(int partidosJugados = 0)//esto para validar si en la jornada se jugo
        {
            if(partidosJugados <= 0)
            {
                return false;//no se jugo
            }
            return true; //se jugo al menos un partido
        }


    }
}


using System.IO.Pipelines;
using Temporada2025.Backend.Data;
using Temporada2025.Backend.DTOs;
using Temporada2025.Backend.Models;

namespace Temporada2025.Backend.Services
{
    public class JugadorService : IJugadorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public JugadorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool,string?)> RegistrarJugador(RegistrarJugadorRequest request)
        {
            var existeJugador = await _unitOfWork.JugadorRepository.ExisteJugadorAsync(
                request.nombre, 
                request.apellidoPaterno, 
                request.apellidoMaterno, 
                request.fechaNacimiento);

            if (existeJugador)
                return (false, "El jugador ya está registrado.");

            if (string.IsNullOrEmpty(request.nombre) ||
                string.IsNullOrEmpty(request.apellidoMaterno) ||
                string.IsNullOrEmpty(request.apellidoMaterno))
                return (false,"No se admite valores nulos");


            if (!ValidarDorsal(request.dorsal))
                return (false, "Dorsal invalida");
            if (!ValidarPie(request.pie))
                return (false, "Pie invalido");
            if (!ValidarPosicion(request.posicion))
                return (false, "Posición invalida");
            if (request.fechaNacimiento >= DateOnly.FromDateTime(DateTime.Now))
                return (false, "Fecha de nacimiento invalida");

            var password = GenerarPassword(request.nombre, request.apellidoPaterno, DateTime.Now);

            Jugador jugador = new Jugador
            {
                Id = Guid.NewGuid(),
                Nombre = request.nombre,
                ApellidoPaterno = request.apellidoPaterno,
                ApellidoMaterno = request.apellidoMaterno,
                Posición = request.posicion,
                Pie = request.pie,
                Dorsal = request.dorsal,
                FechaNacimiento = request.fechaNacimiento,
                Password = password, 
                FechaRegistro = DateTime.Now
            };

            await _unitOfWork.JugadorRepository.AddAsync(jugador);
            
            return (true,password);
        }

        public string GenerarPassword(string nombre, string apellidoPaterno, DateTime fechaRegistro)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoPaterno))
                throw new ArgumentException("Nombre y apellido paterno son obligatorios.");//corregir esto
            var parteNombre = nombre.Length >= 3 ? nombre.Substring(0, 3) : nombre;
            var parteApellido = apellidoPaterno.Length >= 3 ? apellidoPaterno.Substring(0, 3) : apellidoPaterno;
            var parteFecha = fechaRegistro.ToString("ddMMyy");
            var password = $"{parteNombre}{parteApellido}{parteFecha}";
            return password;
        }

       

        public bool ValidarDorsal(int dorsal)
        {
            if(dorsal<=0)
                return false;
            if(dorsal>=100)
                return false;
            return true;
        }

        public bool ValidarPie(string pie)
        {
            string[] opcionesValidas = { "Diestro", "Zurdo", "Ambidiestro" };
            if (string.IsNullOrWhiteSpace(pie))//si esta vacio
                return false;
            if (opcionesValidas.Contains(pie))//si esta dentro de las opciones
                return true;
            return false;
        }

        public bool ValidarPosicion(string posicion)
        {
            string[] opcionesValidas = { "Arquero", "Defensa", "Lateral", "Mediocampista","Extremo","Delantero" };
            if (string.IsNullOrWhiteSpace(posicion))//si esta vacio
                return false;
            if (opcionesValidas.Contains(posicion))//si esta dentro de las opciones
                return true;
            return false;
        }
    }
}

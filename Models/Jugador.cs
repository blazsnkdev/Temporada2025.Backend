namespace Temporada2025.Backend.Models
{
    public class Jugador
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Posición { get; set; }
        public string Pie { get; set; }
        public int Dorsal { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }

        //modelar el usuario se va generar aparitr de la info 
        public string Password { get; set; }//check

        //estadisticas
        public ICollection<Estadistica> Estadisticas { get; set; }

        public Jugador()
        {
        }

        public Jugador(
            Guid id,
            string nombre,
            string apellidoPaterno,
            string apellidoMaterno,
            string posición,
            string pie,
            int dorsal,
            DateOnly fechaNacimiento,
            DateTime fechaRegistro,
            string password)
        {
            Id = id;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Posición = posición;
            Pie = pie;
            Dorsal = dorsal;
            FechaNacimiento = fechaNacimiento;
            FechaRegistro = fechaRegistro;
            Password = password;
        }
    }
}

using System;

namespace SistemaDeReservasBiblioteca.Modelos
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public int IdUsuario { get; set; }
        public string ISBNLibro { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime? FechaRetorno { get; set; }
    }
}

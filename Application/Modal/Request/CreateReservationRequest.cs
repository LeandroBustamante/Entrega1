namespace Application.Models
{
    // DTO de Entrada (Request) - Punto 5 del mensaje del profe
    public class CreateReservationRequest
    {
        public Guid SeatId { get; set; } // UUID según diagrama de base de datos
        public int UserId { get; set; }  // FK al usuario
    }
}
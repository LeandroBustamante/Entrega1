namespace Application.Models
{
    // DTO de Salida (Response) - Punto 5 del mensaje del profe
    public class ReservationResponseDto
    {
        public Guid ReservationId { get; set; }
        public string Message { get; set; } // Para dar retroalimentación visual clara
    }
}
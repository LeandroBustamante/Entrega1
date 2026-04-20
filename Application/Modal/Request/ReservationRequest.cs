using System;

namespace Application.Modal.Request
{
    public class ReservationRequest
    {
        // ID DEL ASIENTO QUE SE DESEA RESERVAR
        public Guid SeatId { get; set; }
        // ID DEL USUARIO QUE REALIZA LA RESERVA (PARA PRUEBAS USAMOS EL 1)
        public int UserId { get; set; }
    }
}
using System;

namespace Domain.Entities
{
    public class Reservation
    {
        // IDENTIFICADOR UNICO TIPO UUID DE LA RESERVA
        public Guid Id { get; set; } = Guid.NewGuid();

        // FK: USUARIO QUE REALIZA LA RESERVA
        public int UserId { get; set; }
        public User User { get; set; }

        // FK: BUTACA RESERVADA
        public Guid SeatId { get; set; }
        public Seat Seat { get; set; }

        // ESTADO: PENDING, PAID, EXPIRED
        public string Status { get; set; }
        // FECHA Y HORA EN QUE SE CREO LA RESERVA
        public DateTime ReservedAt { get; set; }
        // FECHA Y HORA EN QUE EXPIRA EL BLOQUEO (5 MINUTOS)
        public DateTime ExpiresAt { get; set; }
    }
}
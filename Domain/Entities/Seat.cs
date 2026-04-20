using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Seat
    {
        // IDENTIFICADOR UNICO TIPO UUID PARA LA BUTACA
        public Guid Id { get; set; } = Guid.NewGuid();

        // FK: SECTOR AL QUE PERTENECE LA BUTACA
        public int SectorId { get; set; }
        public Sector Sector { get; set; }

        // IDENTIFICADOR DE FILA (EJ. FILA A)
        public string RowIdentifier { get; set; }
        // NUMERO DE ASIENTO DENTRO DE LA FILA
        public int SeatNumber { get; set; }
        // ESTADO: AVAILABLE, RESERVED, SOLD
        public string Status { get; set; }

        // CAMPO PARA CONTROL DE CONCURRENCIA (OPTIMISTIC LOCKING)
        public int Version { get; set; }

        // RELACION: UNA BUTACA SE ASIGNA A UNA RESERVA
        public ICollection<Reservation> Reservations { get; set; }
    }
}
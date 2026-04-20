using System;

namespace Application.Modal.Response
{
    // DTO PARA EL MAPA DE ASIENTOS
    public class SeatResponse
    {
        // EL ID ES UUID PARA EL BLOQUEO TEMPORAL
        public Guid Id { get; set; }
        public string Row { get; set; }
        public int Number { get; set; }
        // ESTADO: AVAILABLE, RESERVED, SOLD
        public string Status { get; set; }
    }
}
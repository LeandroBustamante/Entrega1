using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Sector
    {
        // IDENTIFICADOR UNICO DEL SECTOR
        public int Id { get; set; }

        // FK: RELACION CON EL EVENTO AL QUE PERTENECE
        public int EventId { get; set; }
        public Event Event { get; set; }

        // NOMBRE DEL SECTOR (EJ. PLATEA ALTA, CAMPO)
        public string Name { get; set; }
        // PRECIO DE LAS BUTACAS EN ESTE SECTOR
        public decimal Price { get; set; }
        // CAPACIDAD TOTAL DE ASIENTOS DEL SECTOR
        public int Capacity { get; set; }

        // RELACION: UN SECTOR CONTIENE MUCHAS BUTACAS
        public ICollection<Seat> Seats { get; set; }
    }
}
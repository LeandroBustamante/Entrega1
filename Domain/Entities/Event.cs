using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Event
    {
        // IDENTIFICADOR UNICO DEL EVENTO
        public int Id { get; set; }
        // NOMBRE DEL EVENTO (EJ. CONCIERTO DE ROCK)
        public string Name { get; set; } = string.Empty; // INICIALIZACION PARA EVITAR ADVERTENCIAS
        // FECHA Y HORA EN QUE SE REALIZA EL EVENTO
        public DateTime EventDate { get; set; }
        // LUGAR O RECINTO DONDE SE LLEVA A CABO
        public string Venue { get; set; } = string.Empty;
        // ESTADO ACTUAL DEL EVENTO
        public string Status { get; set; } = string.Empty;

        // RELACION: UN EVENTO TIENE MUCHOS SECTORES
        public ICollection<Sector> Sectors { get; set; }
    }
}
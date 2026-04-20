using System;

namespace Application.Modal.Response
{
    // DTO PARA MOSTRAR LA INFORMACION DEL EVENTO EN EL CATALOGO
    public class EventResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public string Venue { get; set; }
        public string Status { get; set; }
    }
}
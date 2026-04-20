using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Events.GetEvents;
using Application.Modal.Response;
using Application.Interfaces.Information;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IGetEventsHandler _getEventsHandler;
        private readonly IEventService _eventService;

        public EventsController(IGetEventsHandler getEventsHandler, IEventService eventService)
        {
            _getEventsHandler = getEventsHandler;
            _eventService = eventService;
        }

        // 1. Obtener todos los eventos usando el Handler (Caso de Uso)
        [HttpGet]
        public async Task<ActionResult<List<EventResponse>>> GetAll()
        {
            var events = await _getEventsHandler.HandleAsync();
            return Ok(events);
        }

        // 2. Obtener sectores usando [FromRoute] (Punto 2 del profe)
        [HttpGet("{eventId}/sectors")]
        public async Task<ActionResult<List<SectorResponse>>> GetSectors([FromRoute] int eventId)
        {
            var sectors = await _eventService.GetSectorsByEventAsync(eventId);
            return Ok(sectors);
        }

        // 3. Obtener asientos usando [FromQuery] (Punto 2 del profe)
        // Ejemplo de URL: api/v1/Events/seats?sectorId=5
        [HttpGet("seats")]
        public async Task<ActionResult<List<SeatResponse>>> GetSeats([FromQuery] int sectorId)
        {
            var seats = await _eventService.GetSeatsBySectorAsync(sectorId);
            return Ok(seats);
        }
    }
}
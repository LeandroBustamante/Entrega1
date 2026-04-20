using Application.Interfaces.Information;
using Application.Mapper;
using Application.Modal.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class EventService : IEventService
    {
        private readonly IEventQuery _eventQuery;

        // INYECCION DE LA CAPA DE CONSULTA (QUERY)
        public EventService(IEventQuery eventQuery)
        {
            _eventQuery = eventQuery;
        }

        // OBTIENE EVENTOS Y LOS MAPEA A DTO
        public async Task<List<EventResponse>> GetAllEventsAsync()
        {
            var events = await _eventQuery.GetAllEventsAsync();
            return events.Select(EventMapper.ToDTO).ToList();
        }

        // OBTIENE SECTORES Y LOS MAPEA A DTO
        public async Task<List<SectorResponse>> GetSectorsByEventAsync(int eventId)
        {
            var sectors = await _eventQuery.GetSectorsByEventIdAsync(eventId);
            return sectors.Select(SectorMapper.ToDTO).ToList();
        }

        // OBTIENE ASIENTOS Y LOS MAPEA A DTO
        public async Task<List<SeatResponse>> GetSeatsBySectorAsync(int sectorId)
        {
            var seats = await _eventQuery.GetSeatsBySectorIdAsync(sectorId);
            return seats.Select(SeatMapper.ToDTO).ToList();
        }
    }
}
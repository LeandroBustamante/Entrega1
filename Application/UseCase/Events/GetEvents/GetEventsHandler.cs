using Application.Interfaces.Information;
using Application.Modal.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases.Events.GetEvents
{
    public class GetEventsHandler : IGetEventsHandler
    {
        private readonly IEventService _eventService;

        public GetEventsHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<List<EventResponse>> HandleAsync()
        {
            // Usando el nombre real de tu interfaz: GetAllEventsAsync
            return await _eventService.GetAllEventsAsync();
        }
    }
}
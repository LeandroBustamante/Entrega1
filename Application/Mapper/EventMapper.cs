using Application.Modal.Response;
using Domain.Entities;

namespace Application.Mapper
{
    public static class EventMapper
    {
        // TRANSFORMA UNA ENTIDAD EVENT A UN EVENTRESPONSE
        public static EventResponse ToDTO(Event ev)
        {
            return new EventResponse
            {
                Id = ev.Id,
                Name = ev.Name,
                EventDate = ev.EventDate,
                Venue = ev.Venue,
                Status = ev.Status
            };
        }
    }
}
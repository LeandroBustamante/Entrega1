using Application.Modal.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases.Events.GetEvents
{
    public interface IGetEventsHandler
    {
        Task<List<EventResponse>> HandleAsync();
    }
}
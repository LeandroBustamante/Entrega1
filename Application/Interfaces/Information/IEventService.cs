using Application.Modal.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Information
{
    public interface IEventService
    {
        // FIRMA PARA OBTENER TODOS LOS EVENTOS MAPEADOS
        Task<List<EventResponse>> GetAllEventsAsync();
        // FIRMA PARA OBTENER LOS SECTORES DE UN EVENTO
        Task<List<SectorResponse>> GetSectorsByEventAsync(int eventId);
        // FIRMA PARA OBTENER LOS ASIENTOS DE UN SECTOR
        Task<List<SeatResponse>> GetSeatsBySectorAsync(int sectorId);
    }
}
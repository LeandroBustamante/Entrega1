using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Information
{
    public interface IEventQuery
    {
        // OBTENER TODOS LOS EVENTOS DISPONIBLES
        Task<List<Event>> GetAllEventsAsync();

        // OBTENER LOS SECTORES DE UN EVENTO ESPECIFICO
        Task<List<Sector>> GetSectorsByEventIdAsync(int eventId);

        // OBTENER TODAS LAS BUTACAS DE UN SECTOR CON SU ESTADO ACTUAL
        Task<List<Seat>> GetSeatsBySectorIdAsync(int sectorId);

        // VERIFICAR SI UNA BUTACA EXISTE Y ESTA DISPONIBLE
        Task<Seat> GetSeatByIdAsync(Guid seatId);
    }
}
using Application.Interfaces.Information;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class EventQuery : IEventQuery
    {
        private readonly EventContext _context;

        // INYECCION DEL CONTEXTO DE BASE DE DATOS
        public EventQuery(EventContext context)
        {
            _context = context;
        }

        // OBTIENE LA LISTA DE EVENTOS DESDE LA DB SIN HACER SEGUIMIENTO DE CAMBIOS (PARA MEJORAR RENDIMIENTO)
        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events.AsNoTracking().ToListAsync();
        }

        // OBTIENE LOS SECTORES FILTRADOS POR EL ID DEL EVENTO
        public async Task<List<Sector>> GetSectorsByEventIdAsync(int eventId)
        {
            return await _context.Sectors
                .Where(s => s.EventId == eventId)
                .AsNoTracking()
                .ToListAsync();
        }

        // OBTIENE LOS ASIENTOS DE UN SECTOR ESPECIFICO
        public async Task<List<Seat>> GetSeatsBySectorIdAsync(int sectorId)
        {
            return await _context.Seats
                .Where(s => s.SectorId == sectorId)
                .AsNoTracking()
                .ToListAsync();
        }

        // BUSCA UNA BUTACA ESPECIFICA POR SU UUID
        public async Task<Seat> GetSeatByIdAsync(Guid seatId)
        {
            return await _context.Seats
                .FirstOrDefaultAsync(s => s.Id == seatId);
        }
    }
}
using Application.Interfaces.Reservations;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class ReservationCommand : IReservationCommand
    {
        private readonly EventContext _context;

        public ReservationCommand(EventContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateReservationAtomicAsync(Reservation reservation, Seat seat, AuditLog auditLog)
        {
            // INICIAMOS UNA TRANSACCION PARA ASEGURAR ACID (ATOMICIDAD) [cite: 61, 63]
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. ACTUALIZAMOS EL ESTADO DEL ASIENTO
                _context.Seats.Update(seat);

                // 2. AGREGAMOS LA RESERVA
                await _context.Reservations.AddAsync(reservation);

                // 3. AGREGAMOS EL REGISTRO DE AUDITORIA INMUTABLE [cite: 19]
                await _context.AuditLogs.AddAsync(auditLog);

                // GUARDAMOS LOS CAMBIOS EN LA DB
                await _context.SaveChangesAsync();

                // SI TODO SALIO BIEN, CONFIRMAMOS LA TRANSACCION
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                // SI ALGO FALLA, HACEMOS ROLLBACK COMPLETO [cite: 63]
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
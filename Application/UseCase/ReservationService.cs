using Application.Exceptions;
using Application.Interfaces.Information;
using Application.Interfaces.Reservations;
using Application.Modal.Request;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IEventQuery _eventQuery;
        private readonly IReservationCommand _reservationCommand;

        public ReservationService(IEventQuery eventQuery, IReservationCommand reservationCommand)
        {
            _eventQuery = eventQuery;
            _reservationCommand = reservationCommand;
        }

        public async Task<bool> ReserveSeatAsync(ReservationRequest request)
        {
            // 1. BUSCAR LA BUTACA EN LA BASE DE DATOS
            var seat = await _eventQuery.GetSeatByIdAsync(request.SeatId);

            if (seat == null)
                throw new NotFoundException("LA BUTACA SOLICITADA NO EXISTE.");

            // 2. VALIDAR SI LA BUTACA ESTA DISPONIBLE
            if (seat.Status != "Available")
                throw new ConflictException("LA BUTACA YA SE ENCUENTRA OCUPADA O RESERVADA.");

            // 3. PREPARAR LA ACTUALIZACION DEL ASIENTO
            seat.Status = "Reserved";

            // 4. CREAR LA ENTIDAD DE RESERVA CON LOS 5 MINUTOS DE EXPIRACION
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                SeatId = seat.Id,
                Status = "Pending",
                ReservedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(5)
            };

            // 5. CREAR EL REGISTRO DE AUDITORIA (Punto 4: Nombre corregido Audit_log)
            var auditLog = new AuditLog
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Action = "RESERVE_SUCCESS",
                EntityType = "Seat",
                EntityId = seat.Id.ToString(),
                Details = "RESERVA TEMPORAL DE BUTACA EXITOSA",
                CreatedAt = DateTime.Now
            };

            // 6. EJECUTAR TODO EN LA TRANSACCION ATOMICA (Punto 4)
            // Se pasan los 3 objetos para que el Command haga un solo SaveChangesAsync()
            var success = await _reservationCommand.CreateReservationAtomicAsync(reservation, seat, auditLog);

            if (!success)
                throw new Exception("ERROR AL PROCESAR LA RESERVA EN LA BASE DE DATOS.");

            return true;
        }
    }
}
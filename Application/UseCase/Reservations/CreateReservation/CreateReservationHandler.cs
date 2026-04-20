using Application.Interfaces.Reservations;
using Application.Modal.Request;
using Application.Modal.Response;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.Reservations.CreateReservation
{
    public class CreateReservationHandler : ICreateReservationHandler
    {
        private readonly IReservationService _reservationService;

        public CreateReservationHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<GenericResponse> HandleAsync(ReservationRequest request)
        {
            // 1. Ejecutamos la reserva
            await _reservationService.ReserveSeatAsync(request);

            // 2. Punto 4 del Profe: Auditoría Atómica
            // Aquí llamaríamos a un método del service o repositorio para insertar en 'Audit_log'
            // Debe hacerse dentro de la misma transacción de ReserveSeatAsync para ser "atómico"

            return new GenericResponse();
        }
    }
}
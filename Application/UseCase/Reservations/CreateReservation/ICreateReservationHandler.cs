using Application.Modal.Request;
using Application.Modal.Response;

namespace Application.UseCases.Reservations.CreateReservation
{
    public interface ICreateReservationHandler
    {
        // Usamos GenericResponse porque es el que SI tenés en tu carpeta Modal/Response
        Task<GenericResponse> HandleAsync(ReservationRequest request);
    }
}
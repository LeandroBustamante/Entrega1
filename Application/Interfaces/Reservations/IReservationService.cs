using Application.Modal.Request;
using System.Threading.Tasks;

namespace Application.Interfaces.Reservations
{
    public interface IReservationService
    {
        // FIRMA PARA PROCESAR LA RESERVA INICIAL
        Task<bool> ReserveSeatAsync(ReservationRequest request);
    }
}
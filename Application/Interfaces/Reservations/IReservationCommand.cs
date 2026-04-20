using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Reservations
{
    public interface IReservationCommand
    {
        // FIRMA PARA CREAR LA RESERVA, ACTUALIZAR EL ASIENTO Y LOGUEAR LA AUDITORIA EN UNA SOLA TRANSACCION
        Task<bool> CreateReservationAtomicAsync(Reservation reservation, Seat seat, AuditLog auditLog);
    }
}
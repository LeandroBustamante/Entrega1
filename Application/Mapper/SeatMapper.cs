using Application.Modal.Response;
using Domain.Entities;

namespace Application.Mapper
{
    public static class SeatMapper
    {
        // TRANSFORMA UNA ENTIDAD SEAT A UN SEATRESPONSE
        public static SeatResponse ToDTO(Seat s)
        {
            return new SeatResponse
            {
                Id = s.Id,
                Row = s.RowIdentifier,
                Number = s.SeatNumber,
                Status = s.Status
            };
        }
    }
}
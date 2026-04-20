using Application.Modal.Response;
using Domain.Entities;

namespace Application.Mapper
{
    public static class SectorMapper
    {
        // TRANSFORMA UNA ENTIDAD SECTOR A UN SECTORRESPONSE
        public static SectorResponse ToDTO(Sector s)
        {
            return new SectorResponse
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                Capacity = s.Capacity
            };
        }
    }
}
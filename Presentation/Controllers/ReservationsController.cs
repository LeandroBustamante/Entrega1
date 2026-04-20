using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Reservations.CreateReservation;
using Application.Modal.Request;
using Application.Modal.Response;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ICreateReservationHandler _handler;

        // Punto 3: Inyección de dependencia del Handler en el constructor
        public ReservationsController(ICreateReservationHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationRequest request)
        {
            // Si el JSON llega mal formado o faltan datos requeridos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _handler.HandleAsync(request);
                // Si GenericResponse está vacío, devolvé un mensaje manual para probar:
                return Ok(new { message = "RESERVA EXITOSA" });
            }
            catch (Application.Exceptions.NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Application.Exceptions.ConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
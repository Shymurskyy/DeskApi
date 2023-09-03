using Microsoft.AspNetCore.Mvc;

namespace DeskApi.Controllers
{

    [Route("api/desks")]
    [ApiController]
    public class DesksController : ControllerBase
    {
        private readonly IDeskService _deskService;

        public DesksController(IDeskService deskService)
        {
            _deskService = deskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Desk>> GetDesks()
        {
            var desks = _deskService.GetAllDesks();
            return Ok(desks);
        }
        [HttpGet("location/{locationId}")]
        public ActionResult<IEnumerable<Desk>> GetDesksByLocation(int locationId)
        {
            // Query desks by location
            var desksInLocation = _deskService.GetDesksByLocation(locationId);

            if (desksInLocation == null || !desksInLocation.Any())
            {
                return NotFound();
            }

            return Ok(desksInLocation);
        }
        [HttpPost]
        public ActionResult<Desk> CreateDesk(Desk desk)
        {
            var createdDesk = _deskService.CreateDesk(desk);
            return CreatedAtAction(nameof(GetDesks), new { id = createdDesk.DeskId }, createdDesk);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDesk(int id, Desk desk)
        {
            if (!_deskService.UpdateDesk(id, desk))
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDesk(int id)
        {
            if (!_deskService.CanDeleteDesk(id))
            {
                return BadRequest("Cannot delete desk with existing reservations.");
            }

            if (!_deskService.DeleteDesk(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    
    }
    
}

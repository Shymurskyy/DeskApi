using Microsoft.AspNetCore.Mvc;

namespace DeskApi.Controllers
{
    
        [Route("api/locations")]
        [ApiController]
        public class LocationsController : ControllerBase
        {
            private readonly ILocationService _locationService;

            public LocationsController(ILocationService locationService)
            {
                _locationService = locationService;
            }

            [HttpGet]
            public ActionResult<IEnumerable<Location>> GetLocations()
            {
                var locations = _locationService.GetAllLocations();
                return Ok(locations);
            }

            [HttpPost]
            public ActionResult<Location> CreateLocation(Location location)
            {
                var createdLocation = _locationService.CreateLocation(location);
                return CreatedAtAction(nameof(GetLocations), new { id = createdLocation.LocationId }, createdLocation);
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteLocation(int id)
            {
                var locationToDelete = _locationService.GetLocationById(id);
                if (locationToDelete == null)
                {
                    return NotFound();
                }

                if (!_locationService.CanDeleteLocation(id))
                {
                    return BadRequest("Desks exist in this location. Remove the desks first.");
                }

                _locationService.DeleteLocation(id);
                return NoContent();
            }
        }
    
}

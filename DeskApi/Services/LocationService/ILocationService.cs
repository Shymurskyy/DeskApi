using DeskApi.Models;

namespace DeskApi.Services.LocationService
{
    public interface ILocationService
    {
        public IEnumerable<Location> GetAllLocations();
        Location GetLocationById(int locationId);
        Location CreateLocation(Location location);
        bool DeleteLocation(int locationId);
        bool CanDeleteLocation(int locationId);
    }
}

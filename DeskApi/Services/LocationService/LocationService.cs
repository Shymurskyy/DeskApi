using DeskApi.Models;

namespace DeskApi.Services.LocationService
{
    public class LocationService:ILocationService
    {
        private readonly ApplicationDbContext _context;

        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return _context.Locations.ToList();
        }

        public Location GetLocationById(int locationId)
        {
            return _context.Locations.FirstOrDefault(location => location.LocationId == locationId);
        }

        public Location CreateLocation(Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
            return location;
        }


        public bool DeleteLocation(int locationId)
        {
            var locationToRemove = GetLocationById(locationId);
            if (locationToRemove == null)
            {
                return false;
            }

            _context.Locations.Remove(locationToRemove);
            _context.SaveChanges();
            return true;
        }
        public bool CanDeleteLocation(int locationId)
        {
            
            return !_context.Desks.Any(desk => desk.LocationId == locationId);
        }
    }
}

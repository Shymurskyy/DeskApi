
using DeskApi.Models;

namespace DeskApi.Services.DeskService
{
    public class DeskService : IDeskService
    {
        private readonly ApplicationDbContext _context;

        public DeskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Desk> GetAllDesks()
        {
            return _context.Desks.ToList();
        }

        public Desk GetDeskById(int deskId)
        {
            return _context.Desks.FirstOrDefault(desk => desk.DeskId == deskId);
        }
        
        public Desk CreateDesk(Desk desk)
        {
            _context.Desks.Add(desk);
            _context.SaveChanges();
            return desk;
        }
        public IEnumerable<Desk> GetDesksByLocation(int locationId)
        {

            return _context.Desks.Where(desk => desk.LocationId == locationId).ToList();

        }
        public bool UpdateDesk(int deskId, Desk updatedDesk)
        {
            var existingDesk = GetDeskById(deskId);
            if (existingDesk == null)
            {
                return false; // Desk not found
            }

            existingDesk.DeskName = updatedDesk.DeskName;
            existingDesk.IsAvailable = updatedDesk.IsAvailable;

            
            if (!existingDesk.IsAvailable)
            {
                existingDesk.AvailabilityEndDate = updatedDesk.AvailabilityEndDate;
            }
            else
            {
                existingDesk.AvailabilityEndDate = null; 
            }

            _context.SaveChanges();
            return true;
        }

        public bool DeleteDesk(int deskId)
        {
            var deskToRemove = GetDeskById(deskId);
            if (deskToRemove == null)
            {
                return false; 
            }

            _context.Desks.Remove(deskToRemove);
            _context.SaveChanges();
            return true;
        }
        public bool CanDeleteDesk(int deskId)
        {

            return !_context.Reservations.Any(reservation => reservation.DeskId == deskId);
        }
       

    }
}

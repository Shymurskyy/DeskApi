using DeskApi.Models;

namespace DeskApi.Services.ReservationService
{
    public class ReservationService:IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _context.Reservations.ToList();
        }

        public Reservation GetReservationById(int reservationId)
        {
            return _context.Reservations.FirstOrDefault(reservation => reservation.ReservationId == reservationId);
        }
        public IEnumerable<Reservation> GetReservationsForDesk(int deskId)
        {
            return _context.Reservations
                .Where(reservation => reservation.DeskId == deskId)
                .ToList();
        }

        public Reservation CreateReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation;
        }

        public bool UpdateReservation(int reservationId, Reservation updatedReservation)
        {
            var existingReservation = GetReservationById(reservationId);
            if (existingReservation == null)
            {
                return false;
            }

            existingReservation.ReservationDate = updatedReservation.ReservationDate;
            existingReservation.EndDate = updatedReservation.EndDate;
            existingReservation.IsActive = updatedReservation.IsActive;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteReservation(int reservationId)
        {
            var reservationToRemove = GetReservationById(reservationId);
            if (reservationToRemove == null)
            {
                return false;
            }

            _context.Reservations.Remove(reservationToRemove);
            _context.SaveChanges();
            return true;
        }
    }
}

using DeskApi.Models;

namespace DeskApi.Services.ReservationService
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAllReservations();
        Reservation GetReservationById(int reservationId);
        public IEnumerable<Reservation> GetReservationsForDesk(int deskId);
        Reservation CreateReservation(Reservation reservation);
        bool UpdateReservation(int reservationId, Reservation updatedReservation);
        bool DeleteReservation(int reservationId);
    }
}


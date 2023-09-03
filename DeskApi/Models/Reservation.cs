namespace DeskApi.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int EmployeeId { get; set; }
        public int DeskId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}

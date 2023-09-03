namespace DeskApi.Models
{
    public class Desk
    {
        public int DeskId { get; set; }
        public int LocationId { get; set; }
        public string? DeskName { get; set; }
        public DateTime? AvailabilityEndDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public List<Reservation>? Reservations { get; set; }
    }
}

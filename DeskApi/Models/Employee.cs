namespace DeskApi.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Role { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}

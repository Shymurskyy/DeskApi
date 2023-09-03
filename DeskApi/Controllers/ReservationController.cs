using DeskApi.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;

namespace DeskApi.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IDeskService _deskService;
        private readonly IEmployeeService _employeeService;

        public ReservationController( IReservationService reservationService,IDeskService deskService,IEmployeeService employeeService)
        {
            _reservationService = reservationService;
            _deskService = deskService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetReservations()
        {
            var reservations = _reservationService.GetAllReservations();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservation(int id)
        {
            var reservation = _reservationService.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult CreateReservation(Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest("Invalid reservation data.");
            }

            var desk = _deskService.GetDeskById(reservation.DeskId);
            if (desk == null)
            {
                return BadRequest("The selected desk is unavailable.");
            }
            var reservationsForDesk = _reservationService.GetReservationsForDesk(desk.DeskId);
            foreach (var existingReservations in reservationsForDesk)
            {
                if (existingReservations.IsActive &&
                    (existingReservations.ReservationDate <= reservation.ReservationDate &&
                     existingReservations.EndDate >= reservation.ReservationDate))
                {
                    return BadRequest("The selected desk is already reserved for the specified period.");
                }
            }
            if (reservation.ReservationDate.Date < DateTime.Now.Date)
            {
                return BadRequest("Reservations must be for today or a future date.");
            }

            if ((reservation.EndDate - reservation.ReservationDate).TotalDays > 7)
            {
                return BadRequest("Reservations cannot be for more than a week.");
            }
            if (reservation.EndDate < reservation.ReservationDate)
            {
                return BadRequest("Reservation end date cannot be later than start date.");
            }
            var employee = _employeeService.GetEmployeeById(reservation.EmployeeId);
            if (employee == null)
            {
                return BadRequest("Invalid employee.");
            }

            var existingReservation = _reservationService.GetReservationById(reservation.ReservationId);
            if (existingReservation != null)
            {
                var timeDifference = reservation.ReservationDate.Subtract(DateTime.Now);
                if (timeDifference.TotalHours <= 24)
                {
                    return BadRequest("Reservations can only be changed more than 24 hours before the start date.");
                }
            }

            reservation.IsActive = true;
            var createdReservation = _reservationService.CreateReservation(reservation);
            
            desk.IsAvailable = false;
            desk.AvailabilityEndDate = reservation.EndDate;

            _deskService.UpdateDesk(desk.DeskId, desk);

            return CreatedAtAction(nameof(GetReservations), new { id = createdReservation.ReservationId }, createdReservation);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, Reservation reservation)
        {
            if (!_reservationService.UpdateReservation(id, reservation))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            if (!_reservationService.DeleteReservation(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

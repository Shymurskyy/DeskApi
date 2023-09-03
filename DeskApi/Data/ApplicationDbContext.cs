
namespace DeskApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Desk> Desks => Set<Desk>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Employee> Employees => Set<Employee>();

    }
}

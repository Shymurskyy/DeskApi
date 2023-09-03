using DeskApi.Models;

namespace DeskApi.Services.DeskService
{
    public interface IDeskService
    {
        IEnumerable<Desk> GetAllDesks();
        Desk GetDeskById(int deskId);
        IEnumerable<Desk> GetDesksByLocation(int locationId);
        Desk CreateDesk(Desk desk);
        bool UpdateDesk(int deskId, Desk updatedDesk);
        bool DeleteDesk(int deskId);
        bool CanDeleteDesk (int deskId);
    }
}

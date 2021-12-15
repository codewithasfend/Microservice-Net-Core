using ReservationsApi.Models;

namespace ReservationsApi.Interfaces
{
    public interface IReservation
    {
        Task<List<Reservation>> GetReservations();
        Task UpdateMailStatus(int id);
    }
}

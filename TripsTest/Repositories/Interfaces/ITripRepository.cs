using TripsTest.Models;

namespace TripsTest.Repositors.Interfaces
{
    public interface ITripRepository
    {
        Task<IEnumerable<Trip>> GetAllTrips();
    }
}

using TripsTest.Models;

namespace TripsTest.Services.Interfaces
{
    public interface ITripService
    {
        Task<IEnumerable<Trip>> GetAll();
    }
}

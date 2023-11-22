using TripsTest.Models;

namespace TripsTest.Services.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetAllDriversIds();

        Task<IEnumerable<Driver>> GetAllDriversWithPayableTime();
    }
}

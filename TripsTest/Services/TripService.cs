using TripsTest.Models;
using TripsTest.Repositors.Interfaces;
using TripsTest.Services.Interfaces;

namespace TripsTest.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<IEnumerable<Trip>> GetAll()
            => await _tripRepository.GetAllTrips();
    }
}

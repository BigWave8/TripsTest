using TripsTest.Data;
using TripsTest.Models;
using TripsTest.Repositors.Interfaces;

namespace TripsTest.Repositors
{
    internal class TripRepository : ITripRepository
    {
        private readonly TripDBContext _context;

        public TripRepository(TripDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip>> GetAllTrips()
        {
            return _context.Trip;
        }
        //If there will be a lot of data,
        //then in order for the data to be processed on the server,
        //sorting and searching can be implemented here
    }
}

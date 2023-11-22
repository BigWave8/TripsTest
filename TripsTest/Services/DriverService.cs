using System.Collections.Concurrent;
using TripsTest.Models;
using TripsTest.Repositors.Interfaces;
using TripsTest.Services.Interfaces;

namespace TripsTest.Services
{
    public class DriverService : IDriverService
    {
        private readonly ITripRepository _tripRepository;

        public DriverService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<IEnumerable<Driver>> GetAllDriversIds()
        {
            IEnumerable<Trip> trips = await _tripRepository.GetAllTrips();
            var drivers = trips
                .GroupBy(t => t.DriverId)
                .Select(group => new Driver
                {
                    Id = group.Key,
                    PayableTime = null
                });

            return drivers;
        }

        public async Task<IEnumerable<Driver>> GetAllDriversWithPayableTime()
        {
            IEnumerable<Trip> trips = await _tripRepository.GetAllTrips();

            var groupedAndSortedTrips = trips
                .GroupBy(t => t.DriverId)
                .Select(group => new
                {
                    DriverId = group.Key,
                    Trips = group.OrderByDescending(group => group.DropoffTime) //It is not necessary if all the data were real and would be recorded consecutively after each exit from the transport
                }); 

            ConcurrentBag<Driver> drivers = new();

            //Also, with large data scales, different days can be processed in parallel, if necessary
            Parallel.ForEach(groupedAndSortedTrips, (group) =>
            {
                var payableTime = 0.0;
                var currentStartTime = DateTime.MaxValue;
                foreach (var trip in group.Trips)
                {
                    if (trip.PickupTime >= currentStartTime)
                    {
                        continue;
                    }
                    if (trip.DropoffTime > currentStartTime)
                    {
                        payableTime += FullPassangerTime(trip) - TimeDifferenceInMinutes(trip.DropoffTime, currentStartTime);
                    }
                    else
                    {
                        payableTime += FullPassangerTime(trip);
                    }
                    currentStartTime = trip.PickupTime;
                }
                drivers.Add(new Driver()
                {
                    Id = group.DriverId,
                    PayableTime = payableTime
                });
            });

            await Task.WhenAll();

            return drivers;
        }

        private static double FullPassangerTime(Trip trip)
            => TimeDifferenceInMinutes(trip.DropoffTime, trip.PickupTime);

        private static double TimeDifferenceInMinutes(DateTime end, DateTime start)
            => Math.Max(0, (end - start).TotalMinutes);
    }
}

using FluentAssertions;
using Moq;
using TripsTest.Models;
using TripsTest.Repositors.Interfaces;
using TripsTest.Services;
using TripsTest.Services.Interfaces;

namespace TripTest.UnitTests.Services
{
    [TestFixture]
    internal class DriverServiceTests
    {
        private Mock<ITripRepository> _tripRepositoryMock;
        private IDriverService _driverService;

        [SetUp]
        public void Setup()
        {
            _tripRepositoryMock = new Mock<ITripRepository>(MockBehavior.Strict);
            _driverService = new DriverService(_tripRepositoryMock.Object);
        }

        [Test]
        public void GetAllDriversWithPayableTime_EmptyData_Empty()
        {
            SetupTripRepositoryMock(EmptyData());

            var result = _driverService.GetAllDriversWithPayableTime().Result;

            result.Should().BeEmpty();
        }

        [Test]
        public void GetAllDriversWithPayableTime_TwoPassangersAtTheSameTime_10()
        {
            SetupTripRepositoryMock(DataTwoPassangersAtTheSameTime());

            var result = _driverService.GetAllDriversWithPayableTime().Result;

            result.First().PayableTime.Should().Be(10.0d);
        }

        [Test]
        public void GetAllDriversWithPayableTime_TwoPassangersAtDifferentTime_20()
        {
            SetupTripRepositoryMock(DataTwoPassangersAtDifferentTime());

            var result = _driverService.GetAllDriversWithPayableTime().Result;

            result.First().PayableTime.Should().Be(20.0d);
        }

        [Test]
        public void GetAllDriversWithPayableTime_TwoPassangersWithFullTimeOverlap_30()
        {
            SetupTripRepositoryMock(DataTwoPassangersWithFullTimeOverlap());

            var result = _driverService.GetAllDriversWithPayableTime().Result;

            result.First().PayableTime.Should().Be(30.0d);
        }

        [Test]
        public void GetAllDriversWithPayableTime_TwoPassangersWithTimeUnion_15()
        {
            SetupTripRepositoryMock(DataTwoPassangersWithTimeUnion());

            var result = _driverService.GetAllDriversWithPayableTime().Result;

            result.First().PayableTime.Should().Be(15.0d);
        }

        [Test]
        public void GetAllDriversWithPayableTime_TwoDrivers_TwoDriversWithPayableTime10And20()
        {
            double[] expectedResult = { 10.0, 20.0 };
            SetupTripRepositoryMock(DataTwoDrivers());

            var result = _driverService.GetAllDriversWithPayableTime().Result.ToList();

            result[0].PayableTime.Should().BeOneOf(expectedResult);
            result[1].PayableTime.Should().BeOneOf(expectedResult);
        }

        [Test]
        public void GetAllDriversWithPayableTime_TwoDriversWithPassangerAtTheSameTime_TwoDriversWithPayableTime10()
        {
            SetupTripRepositoryMock(DataTwoDriversWithPassangerAtTheSameTime());

            var result = _driverService.GetAllDriversWithPayableTime().Result.ToList();

            result[0].PayableTime.Should().Be(10.0d);
            result[1].PayableTime.Should().Be(10.0d);
        }

        private void SetupTripRepositoryMock(IEnumerable<Trip> data)
            => _tripRepositoryMock.Setup(r => r.GetAllTrips()).Returns(Task.FromResult(data));

        #region Data for tests
        private static IEnumerable<Trip> EmptyData()
        {
            return new List<Trip>();
        }

        private static IEnumerable<Trip> DataTwoPassangersAtTheSameTime()
        {
            var time = DateTime.Now;
            return new List<Trip>
            {
                new Trip() { Id = 1, DriverId = 1, PickupTime = time, DropoffTime = time.AddMinutes(10)},
                new Trip() { Id = 2, DriverId = 1, PickupTime = time, DropoffTime = time.AddMinutes(10)}
            };
        }

        private static IEnumerable<Trip> DataTwoPassangersAtDifferentTime()
        {
            var time = DateTime.Now;
            return new List<Trip>
            {
                new Trip() { Id = 1, DriverId = 1, PickupTime = time, DropoffTime = time.AddMinutes(10)},
                new Trip() { Id = 2, DriverId = 1, PickupTime = time.AddHours(1), DropoffTime = time.AddHours(1).AddMinutes(10)}
            };
        }

        private static IEnumerable<Trip> DataTwoPassangersWithFullTimeOverlap()
        {
            var time = DateTime.Now;
            return new List<Trip>
            {
                new Trip() { Id = 1, DriverId = 1, PickupTime = time, DropoffTime = time.AddMinutes(30)},
                new Trip() { Id = 2, DriverId = 1, PickupTime = time.AddMinutes(10), DropoffTime = time.AddMinutes(20)}
            };
        }

        private static IEnumerable<Trip> DataTwoPassangersWithTimeUnion()
        {
            var time = DateTime.Now;
            return new List<Trip>
            {
                new Trip() { Id = 1, DriverId = 1, PickupTime = time, DropoffTime = time.AddMinutes(10)},
                new Trip() { Id = 2, DriverId = 1, PickupTime = time.AddMinutes(5), DropoffTime = time.AddMinutes(15)}
            };
        }

        private static IEnumerable<Trip> DataTwoDrivers()
        {
            var time = DateTime.Now;
            return new List<Trip>
            {
                new Trip() { Id = 1, DriverId = 1, PickupTime = time, DropoffTime = time.AddMinutes(10)},
                new Trip() { Id = 2, DriverId = 2, PickupTime = time.AddDays(1), DropoffTime = time.AddDays(1).AddMinutes(20)}
            };
        }

        private static IEnumerable<Trip> DataTwoDriversWithPassangerAtTheSameTime()
        {
            var time = DateTime.Now;
            return new List<Trip>
            {
                new Trip() { Id = 1, DriverId = 1, PickupTime = time, DropoffTime = time.AddMinutes(10)},
                new Trip() { Id = 2, DriverId = 2, PickupTime = time, DropoffTime = time.AddMinutes(10)}
            };
        }
        #endregion 
    }
}

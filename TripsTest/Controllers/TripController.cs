using Microsoft.AspNetCore.Mvc;
using TripsTest.Models;
using TripsTest.Services.Interfaces;

namespace TripsTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAll()
        {
            IEnumerable<Trip> trips = await _tripService.GetAll();

            return Ok(trips);
        }
    }
}

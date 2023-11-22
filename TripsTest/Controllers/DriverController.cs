using Microsoft.AspNetCore.Mvc;
using TripsTest.Models;
using TripsTest.Services.Interfaces;

namespace TripsTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("GetAllDriversIds")]
        public async Task<ActionResult<IEnumerable<Driver>>> GetAllDriversIds()
        {
            IEnumerable<Driver> drivers = await _driverService.GetAllDriversIds();

            return Ok(drivers);
        }

        [HttpGet("GetAllDriversWithPayableTime")]
        public async Task<ActionResult<IEnumerable<Driver>>> GetAllDriversWithPayableTime()
        {
            IEnumerable<Driver> drivers = await _driverService.GetAllDriversWithPayableTime();

            return Ok(drivers);
        }
    }
}

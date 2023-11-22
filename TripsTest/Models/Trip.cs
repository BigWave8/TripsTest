using System.ComponentModel.DataAnnotations.Schema;

namespace TripsTest.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Column("driver_id")]
        public int DriverId { get; set; }

        [Column("pickup")]
        public DateTime PickupTime { get; set; }

        [Column("dropoff")]
        public DateTime DropoffTime { get; set; }
    }
}

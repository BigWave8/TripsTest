using Microsoft.EntityFrameworkCore;
using TripsTest.Models;

namespace TripsTest.Data
{
    public class TripDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public TripDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Trip> Trip { get; set; }
    }
}

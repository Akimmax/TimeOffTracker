using Microsoft.EntityFrameworkCore;
using TOT.Entities.Request_Entities;

namespace TOT.Data
{
    public class TOTDBContext : DbContext
    {
        public TOTDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TimeOffRequest> TimeOffRequests { get;}
        public DbSet<TimeOffType> TimeOffTypes { get;}
        public DbSet<Check> Checks { get;}
        public DbSet<RequestStatus> RequestStatuses { get;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

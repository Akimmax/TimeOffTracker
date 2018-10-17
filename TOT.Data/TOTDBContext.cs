using Microsoft.EntityFrameworkCore;
using TOT.Entities.Request_Entities;

namespace TOT.Data
{
    public class TOTDBContext : DbContext
    {
        public TOTDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TimeOffRequest> TimeOffRequests { get; }
        public DbSet<TimeOffType> TimeOffTypes { get; }
        public DbSet<Check> Checks { get; }
        public DbSet<RequestStatus> RequestStatuses { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeOffRequest>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<TimeOffRequest>()
                .HasOne(x => x.TimeOffType)
                .WithMany()
                .IsRequired();

            modelBuilder.Entity<TimeOffType>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<TimeOffType>()
                .Property(x => x.Title)
                .IsRequired();

            modelBuilder.Entity<Check>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Check>()
                .HasOne(x => x.Status)
                .WithMany()
                .IsRequired();
            modelBuilder.Entity<Check>()
                .HasOne(x => x.TimeOffRequest)
                .WithMany(x => x.Checks)
                .IsRequired();

            modelBuilder.Entity<RequestStatus>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<RequestStatus>()
                .Property(x => x.Title)
                .IsRequired();
            modelBuilder.Entity<RequestStatus>()
                .HasData(
                new RequestStatus { Title = "Requsted", Id = (int)Statuses.Requsted },
                new RequestStatus { Title = "In progres", Id = (int)Statuses.InProgres },
                new RequestStatus { Title = "Denied", Id = (int)Statuses.Denied },
                new RequestStatus { Title = "Accepted", Id = (int)Statuses.Accepted });

            base.OnModelCreating(modelBuilder);
        }
    }
}

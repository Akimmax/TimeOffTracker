using Microsoft.EntityFrameworkCore;
using TOT.Entities.TimeOffRequests;
using TOT.Entities.TimeOffPolicies;

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

        public DbSet<Positions> Positions { get; }
        public DbSet<TimeMeasures> TimeMeasures { get; }
        public DbSet<TimeOffPolicy> TimeOffPolicies { get; }
        public DbSet<AccrualSchedule> AccrualSchedules { get; }
        public DbSet<TimeOffPolicyCheckers> TimeOffPolicyCheckers { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //--------------------------Request_Entities----------------------
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

            //--------------------------Policy_Entities----------------------

            modelBuilder.Entity<Positions>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Positions>()
                .Property(x=>x.Title)
                .IsRequired();

            modelBuilder.Entity<TimeMeasures>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<TimeMeasures>()
                .Property(x => x.Title)
                .IsRequired();
            modelBuilder.Entity<TimeMeasures>()
                .HasData(
                new TimeMeasures { Title = "Hours", Id = 1 },
                new TimeMeasures { Title = "Days", Id = 2 },
                new TimeMeasures { Title = "Month", Id = 3 },
                new TimeMeasures { Title = "Years", Id = 4 });

            modelBuilder.Entity<TimeOffPolicy>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<TimeOffPolicy>()
               .HasOne(x=>x.TimeOffType)
               .WithMany()
               .IsRequired();
            modelBuilder.Entity<TimeOffPolicy>()
               .HasOne(x => x.Position)
               .WithMany()
               .IsRequired();
            modelBuilder.Entity<TimeOffPolicy>()
               .HasMany(x=>x.AccrualSchedules)
               .WithOne();
            modelBuilder.Entity<TimeOffPolicy>()
               .HasMany(x => x.TimeOffPolicyCheckers)
               .WithOne()
               .IsRequired();

            modelBuilder.Entity<AccrualSchedule>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<AccrualSchedule>()
               .HasOne(x => x.TimeMeasure)
               .WithMany()
               .IsRequired();

            modelBuilder.Entity<TimeOffPolicyCheckers>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<TimeOffPolicyCheckers>()
               .HasOne(x => x.Position)
               .WithMany()
               .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TOT.Entities.IdentityEntities;
using Pomelo.EntityFrameworkCore.MySql;
using TOT.Entities;
using TOT.Entities.TimeOffRequests;
using TOT.Entities.TimeOffPolicies;
using System.ComponentModel.DataAnnotations.Schema;

namespace TOT.Data
{
    public class TOTDBContext : IdentityDbContext<User>
    {
        public TOTDBContext(DbContextOptions<TOTDBContext> options) : base(options)
        {
        }

        public DbSet<TimeOffPolicy> TimeOffTypes { get; }
        public DbSet<TimeOffRequest> TimeOffRequests { get; }
        public DbSet<TimeOffRequestApproval> TimeOffRequestApprovals { get; }
        public DbSet<TimeOffRequestApprovalStatuses> TimeOffRequestApprovalStatuses { get; }

        public DbSet<EmployeePosition> Positions { get; }
        public DbSet<TimeOffPolicy> TimeOffPolicies { get; }
        public DbSet<TimeOffPolicyApproval> TimeOffPolicyApprovals { get; }
        public DbSet<EmployeePositionTimeOffPolicy> EmployeePositionTimeOffPolicies { get; }
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //--------------------------Shared--------------------------------
            modelBuilder.Entity<EmployeePosition>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<EmployeePosition>()
                .Property(x => x.Title)
                .IsRequired();

            modelBuilder.Entity<TimeOffType>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<TimeOffType>()
                .Property(x=>x.Id)
                .ValueGeneratedNever();
            modelBuilder.Entity<TimeOffType>()
                .Property(x => x.Title)
                .IsRequired();
            modelBuilder.Entity<TimeOffType>()
                .HasData(
                new TimeOffType() { Title = "PayedTimeOff", Id = (int)TimeOffTypeEnum.PayedTimeOff });

            //--------------------------TimeOffRequests----------------------
            modelBuilder.Entity<TimeOffRequest>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<TimeOffRequest>()
                .HasOne(x => x.Policy)
                .WithMany()
                .IsRequired();
            modelBuilder.Entity<TimeOffRequest>()
                .HasOne(x => x.Type)
                .WithMany()
                .IsRequired();
            modelBuilder.Entity<TimeOffRequest>()
                .HasMany(x => x.Approvals)
                .WithOne(x=>x.TimeOffRequest)
                .HasForeignKey(x=>x.TimeOffRequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<TimeOffRequestApproval>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<TimeOffRequestApproval>()
                .HasOne(x => x.Status)
                .WithMany()
                .IsRequired();

            modelBuilder.Entity<TimeOffRequestApprovalStatuses>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<TimeOffRequestApprovalStatuses>()
                .Property(x => x.Id)
                .ValueGeneratedNever();
            modelBuilder.Entity<TimeOffRequestApprovalStatuses>()
                .Property(x => x.Title)
                .IsRequired();
            modelBuilder.Entity<TimeOffRequestApprovalStatuses>()
                .HasData(
                new TimeOffRequestApprovalStatuses() { Title = "Requested", Id = (int)TimeOffRequestApprovalStatusesEnum.Requested },
                new TimeOffRequestApprovalStatuses() { Title = "In progres", Id = (int)TimeOffRequestApprovalStatusesEnum.InProgres },
                new TimeOffRequestApprovalStatuses() { Title = "Denied", Id = (int)TimeOffRequestApprovalStatusesEnum.Denied },
                new TimeOffRequestApprovalStatuses() { Title = "Accepted", Id = (int)TimeOffRequestApprovalStatusesEnum.Accepted });

            //--------------------------TimeOffEntities----------------------
            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasOne(x => x.Policy)
               .WithMany()
               .IsRequired();
            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasOne(x => x.Position)
               .WithMany()
               .IsRequired();
            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasMany(x=>x.Approvals)
               .WithOne();
            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasOne(x=>x.Type)
               .WithMany()
               .IsRequired();

            modelBuilder.Entity<TimeOffPolicy>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<TimeOffPolicyApproval>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<TimeOffPolicyApproval>()
               .HasOne(x => x.Position)
               .WithMany()
               .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}

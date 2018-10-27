using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TOT.Entities.IdentityEntities;
using TOT.Entities;
using TOT.Entities.TimeOffRequests;
using TOT.Entities.TimeOffPolicies;
using System.Collections.Generic;

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

        public DbSet<TimeOffPolicy> TimeOffPolicies { get; }
        public DbSet<EmployeePosition> EmployeePositions { get; }
        public DbSet<TimeOffPolicyApproval> TimeOffPolicyApprovals { get; }
        public DbSet<EmployeePositionTimeOffPolicy> EmployeePositionTimeOffPolicies { get; }
        public DbSet<EmployeePositionTimeOffPolicyNotes> EmployeePositionTimeOffPolicyNotes { get; }
 

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
                new TimeOffType() { Title = "Paid Holiday", Id = (int)TimeOffTypeEnum.PaidHoliday },
                new TimeOffType() { Title = "Unpaid leave", Id = (int)TimeOffTypeEnum.UnpaidLeave },
                new TimeOffType() { Title = "Study Holiday", Id = (int)TimeOffTypeEnum.StudyHoliday },
                new TimeOffType() { Title = "Sick leave", Id = (int)TimeOffTypeEnum.SickLeave});

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
            modelBuilder.Entity<EmployeePositionTimeOffPolicyNotes>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<EmployeePositionTimeOffPolicyNotes>()
                .Property(x => x.Name)
                .IsRequired();
            modelBuilder.Entity<EmployeePositionTimeOffPolicyNotes>()
                .Property(x => x.Note)
                .IsRequired();

            modelBuilder.Entity<TimeOffPolicy>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<TimeOffPolicyApproval>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<TimeOffPolicyApproval>()
               .HasOne(x => x.EmployeePosition)
               .WithMany()
               .HasForeignKey(x=>x.EmployeePositionId)
               .IsRequired();

            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasOne(x => x.Policy)
               .WithMany()
               .HasForeignKey(x=>x.PolicyId)
               .IsRequired();
            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasOne(x => x.Position)
               .WithMany();
            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasMany(x=>x.Approvals)
               .WithOne()
               .HasForeignKey(x=>x.EmployeePositionTimeOffPolicyId);
            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasOne(x=>x.Type)
               .WithMany()
               .HasForeignKey(x=>x.TypeId)
               .IsRequired();
            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
               .HasOne(x => x.Note)
               .WithMany()
               .HasForeignKey(x=>x.NoteId)
               .IsRequired();
            //--------------------Data Initializing----------------------
            modelBuilder.Entity<EmployeePosition>()
                .HasData(
                new EmployeePosition()
                {
                    Id = 1,
                    Title = "Admin"
                });

            modelBuilder.Entity<EmployeePositionTimeOffPolicyNotes>()
                .HasData(
                new EmployeePositionTimeOffPolicyNotes()
                {
                    Id = 1,
                    Name = "������������ ������",
                    Note =
                "����� ����������� 20 ������� ���� ������� � ���."
                },
                new EmployeePositionTimeOffPolicyNotes()
                {
                Id = 2,
                    Name = "���������������� (��������������) ������",
                    Note =
                "����������� �������� ����������� ����� �� ����� 15 ������� ���� � ��� ��������������� �������."
                },
                new EmployeePositionTimeOffPolicyNotes()
                {
                    Id = 3,
                    Name = "������� ������",
                    Note =
                "������� ������ ����� ����� ������ � ������ ������, ������ ��� �����������," +
                " ��� ������ �� ������� ���������, � ���������� ��� 6 ������� � ������� ����������� �������������� �����.<br>" +
                " ��������� ���������� ���� �������� ������� �� ����� ��������� 10 ������� ���� � ���.<br>" +
                " �� ���� ������ ������ ����� ������ 5 ������� ���� �������."
                },
                new EmployeePositionTimeOffPolicyNotes()
                {
                    Id = 4,
                    Name = "����������",
                    Note =
                "��� 1-2 ���� ���������� �� ������� ����� �� ����� ����������� ���������� ����. ����� ���� ����� ���� �� ����� 7 � ���.<br>"+
                "������ 10 ��������� ������� ���� � ����," +
                " ����������� �� ����������(��� �� ����������� �����, ��� � ��� ����) �������� ���������� � ������ 100 % �������.<br>" +
                "��������� 10 ��������� ������� ���� � ����," +
                " ����������� �� ����������(��� �� ����������� �����, ��� � ��� ����) �������� ���������� � 50 % �������.<br>" +
                "��������� 10 ��������� ������� ���� � ����," +
                " ����������� �� ����������(��� �� ����������� �����, ��� � ��� ����) �������� ���������� � 25 % �������.<br>" +
                "��� ����������� ��� ����� 30 ������� ���� �������� �� ����������."
                }
                );

            modelBuilder.Entity<TimeOffPolicy>()
                .HasData(
                new TimeOffPolicy()
                {
                    Id = 1,
                    Name = "������������ ������",
                    ResetDate = new System.DateTime(1, 1, 1),
                    TimeOffDaysPerYear = 20
                },
                new TimeOffPolicy()
                {
                    Id = 2,
                    Name = "���������������� (��������������) ������",
                    ResetDate = new System.DateTime(1, 1, 1),
                    TimeOffDaysPerYear = 15
                },
                new TimeOffPolicy()
                {
                    Id = 3,
                    Name = "������� ������",
                    ResetDate = new System.DateTime(1, 1, 1),
                    TimeOffDaysPerYear = 10
                },
                new TimeOffPolicy()
                {
                    Id = 4,
                    Name = "����������",
                    ResetDate = new System.DateTime(1, 1, 1),
                    TimeOffDaysPerYear = 30
                }
                );

            modelBuilder.Entity<TimeOffPolicyApproval>()
                .HasData(
                new TimeOffPolicyApproval()
                {
                    Id = 1,
                    Amount = 1,
                    EmployeePositionId =1,
                    UserId = null,
                    EmployeePositionTimeOffPolicyId =1
                },
                new TimeOffPolicyApproval()
                {
                    Id = 2,
                    Amount = 1,
                    EmployeePositionId = 1,
                    UserId = null,
                    EmployeePositionTimeOffPolicyId = 2
                },
                new TimeOffPolicyApproval()
                {
                    Id = 3,
                    Amount = 1,
                    EmployeePositionId = 1,
                    UserId = null,
                    EmployeePositionTimeOffPolicyId = 3
                },
                new TimeOffPolicyApproval()
                {
                    Id = 4,
                    Amount = 1,
                    EmployeePositionId = 1,
                    UserId = null,
                    EmployeePositionTimeOffPolicyId = 4
                }
                );

            modelBuilder.Entity<EmployeePositionTimeOffPolicy>()
                .HasData(
                new EmployeePositionTimeOffPolicy()
                {
                    Id = 1,
                    TypeId = (int)TimeOffTypeEnum.PaidHoliday,
                    PolicyId = 1,
                    NoteId = 1,
                },
                new EmployeePositionTimeOffPolicy()
                {
                    Id = 2,
                    TypeId = (int)TimeOffTypeEnum.UnpaidLeave,
                    PolicyId = 2,
                    NoteId = 2,
                },
                new EmployeePositionTimeOffPolicy()
                {
                    Id = 3,
                    TypeId = (int)TimeOffTypeEnum.StudyHoliday,
                    PolicyId = 3,
                    NoteId = 3,
                },
                new EmployeePositionTimeOffPolicy()
                {
                    Id = 4,
                    TypeId = (int)TimeOffTypeEnum.SickLeave,
                    PolicyId = 4,
                    NoteId = 4,
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}

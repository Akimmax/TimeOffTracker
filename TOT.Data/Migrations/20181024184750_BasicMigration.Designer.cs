﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TOT.Data;

namespace TOT.Data.Migrations
{
    [DbContext(typeof(TOTDBContext))]
    [Migration("20181024184750_BasicMigration")]
    partial class BasicMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TOT.Entities.EmployeePosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Title");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("TOT.Entities.TimeOffPolicies.EmployeePositionTimeOffPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PolicyId")
                        .IsRequired();

                    b.Property<int?>("PositionId")
                        .IsRequired();

                    b.Property<int?>("TypeId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PolicyId");

                    b.HasIndex("PositionId");

                    b.HasIndex("TypeId");

                    b.ToTable("EmployeePositionTimeOffPolicies");
                });

            modelBuilder.Entity("TOT.Entities.TimeOffPolicies.TimeOffPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<DateTime>("ResetDate");

                    b.Property<int>("TimeOffDaysPerYear");

                    b.HasKey("Id");

                    b.ToTable("TimeOffPolicy");
                });

            modelBuilder.Entity("TOT.Entities.TimeOffPolicies.TimeOffPolicyApproval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int?>("EmployeePositionTimeOffPolicyId");

                    b.Property<int?>("PositionId")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EmployeePositionTimeOffPolicyId");

                    b.HasIndex("PositionId");

                    b.ToTable("TimeOffPolicyApprovals");
                });

            modelBuilder.Entity("TOT.Entities.TimeOffRequests.TimeOffRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("EndsOn");

                    b.Property<string>("Note");

                    b.Property<int?>("PolicyId")
                        .IsRequired();

                    b.Property<DateTime>("StartsAt");

                    b.Property<int?>("TypeId")
                        .IsRequired();

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.HasIndex("PolicyId");

                    b.HasIndex("TypeId");

                    b.ToTable("TimeOffRequests");
                });

            modelBuilder.Entity("TOT.Entities.TimeOffRequests.TimeOffRequestApproval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Reason");

                    b.Property<DateTime?>("SolvedDate");

                    b.Property<int?>("StatusId")
                        .IsRequired();

                    b.Property<int>("TimeOffRequestId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("TimeOffRequestId");

                    b.ToTable("TimeOffRequestApprovals");
                });

            modelBuilder.Entity("TOT.Entities.TimeOffRequests.TimeOffRequestApprovalStatuses", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("TimeOffRequestApprovalStatuses");

                    b.HasData(
                        new { Id = 1, Title = "Requested" },
                        new { Id = 2, Title = "In progres" },
                        new { Id = 3, Title = "Denied" },
                        new { Id = 4, Title = "Accepted" }
                    );
                });

            modelBuilder.Entity("TOT.Entities.TimeOffRequests.TimeOffType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("TimeOffType");

                    b.HasData(
                        new { Id = 1, Title = "PayedTimeOff" }
                    );
                });

            modelBuilder.Entity("TOT.Entities.TimeOffPolicies.EmployeePositionTimeOffPolicy", b =>
                {
                    b.HasOne("TOT.Entities.TimeOffPolicies.TimeOffPolicy", "Policy")
                        .WithMany()
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TOT.Entities.EmployeePosition", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TOT.Entities.TimeOffRequests.TimeOffType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TOT.Entities.TimeOffPolicies.TimeOffPolicyApproval", b =>
                {
                    b.HasOne("TOT.Entities.TimeOffPolicies.EmployeePositionTimeOffPolicy")
                        .WithMany("Approvals")
                        .HasForeignKey("EmployeePositionTimeOffPolicyId");

                    b.HasOne("TOT.Entities.EmployeePosition", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TOT.Entities.TimeOffRequests.TimeOffRequest", b =>
                {
                    b.HasOne("TOT.Entities.TimeOffPolicies.EmployeePositionTimeOffPolicy", "Policy")
                        .WithMany()
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TOT.Entities.TimeOffRequests.TimeOffType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TOT.Entities.TimeOffRequests.TimeOffRequestApproval", b =>
                {
                    b.HasOne("TOT.Entities.TimeOffRequests.TimeOffRequestApprovalStatuses", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TOT.Entities.TimeOffRequests.TimeOffRequest", "TimeOffRequest")
                        .WithMany("Approvals")
                        .HasForeignKey("TimeOffRequestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

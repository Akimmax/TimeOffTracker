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
    [Migration("20181124160627_RolesInitializerMigration")]
    partial class RolesInitializerMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TOT.Entities.EmployeePosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("EmployeePositions");

                    b.HasData(
                        new { Id = 2, Title = "Employee" },
                        new { Id = 1, Title = "Admin" }
                    );
                });

            modelBuilder.Entity("TOT.Entities.IdentityEntities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<DateTime>("HireDate");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Patronymic");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("PositionId");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("PositionId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TOT.Entities.TimeOffPolicies.EmployeePositionTimeOffPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<int?>("NextPolicyId");

                    b.Property<int>("PolicyId");

                    b.Property<int?>("PositionId");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("NextPolicyId");

                    b.HasIndex("PolicyId");

                    b.HasIndex("PositionId");

                    b.HasIndex("TypeId");

                    b.ToTable("EmployeePositionTimeOffPolicies");

                    b.HasData(
                        new { Id = 1, IsActive = true, PolicyId = 1, TypeId = 1 },
                        new { Id = 2, IsActive = true, PolicyId = 2, TypeId = 2 },
                        new { Id = 3, IsActive = true, PolicyId = 3, TypeId = 3 },
                        new { Id = 4, IsActive = true, PolicyId = 4, TypeId = 4 }
                    );
                });

            modelBuilder.Entity("TOT.Entities.TimeOffPolicies.TimeOffPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DelayBeforeAvailable");

                    b.Property<string>("Name");

                    b.Property<int>("TimeOffDaysPerYear");

                    b.HasKey("Id");

                    b.ToTable("TimeOffPolicies");

                    b.HasData(
                        new { Id = 1, DelayBeforeAvailable = 12, Name = "Paid vacation", TimeOffDaysPerYear = 20 },
                        new { Id = 2, Name = "Unpaid vacation", TimeOffDaysPerYear = 15 },
                        new { Id = 3, DelayBeforeAvailable = 6, Name = "Study vacation", TimeOffDaysPerYear = 10 },
                        new { Id = 4, Name = "Sick vacation", TimeOffDaysPerYear = 30 }
                    );
                });

            modelBuilder.Entity("TOT.Entities.TimeOffPolicies.TimeOffPolicyApprover", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int>("EmployeePositionId");

                    b.Property<int>("EmployeePositionTimeOffPolicyId");

                    b.HasKey("Id");

                    b.HasIndex("EmployeePositionId");

                    b.HasIndex("EmployeePositionTimeOffPolicyId");

                    b.ToTable("TimeOffPolicyApprovals");

                    b.HasData(
                        new { Id = 1, Amount = 1, EmployeePositionId = 1, EmployeePositionTimeOffPolicyId = 1 },
                        new { Id = 2, Amount = 1, EmployeePositionId = 1, EmployeePositionTimeOffPolicyId = 2 },
                        new { Id = 3, Amount = 1, EmployeePositionId = 1, EmployeePositionTimeOffPolicyId = 3 },
                        new { Id = 4, Amount = 1, EmployeePositionId = 1, EmployeePositionTimeOffPolicyId = 4 }
                    );
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

                    b.Property<int>("TypeId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PolicyId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

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

                    b.HasIndex("UserId");

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

                    b.ToTable("TimeOffTypes");

                    b.HasData(
                        new { Id = 1, Title = "Paid Holiday" },
                        new { Id = 2, Title = "Unpaid leave" },
                        new { Id = 3, Title = "Study Holiday" },
                        new { Id = 4, Title = "Sick leave" }
                    );
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TOT.Entities.IdentityEntities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TOT.Entities.IdentityEntities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TOT.Entities.IdentityEntities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TOT.Entities.IdentityEntities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TOT.Entities.IdentityEntities.User", b =>
                {
                    b.HasOne("TOT.Entities.EmployeePosition", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TOT.Entities.TimeOffPolicies.EmployeePositionTimeOffPolicy", b =>
                {
                    b.HasOne("TOT.Entities.TimeOffPolicies.EmployeePositionTimeOffPolicy", "NextPolicy")
                        .WithMany()
                        .HasForeignKey("NextPolicyId");

                    b.HasOne("TOT.Entities.TimeOffPolicies.TimeOffPolicy", "Policy")
                        .WithMany()
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TOT.Entities.EmployeePosition", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");

                    b.HasOne("TOT.Entities.TimeOffRequests.TimeOffType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TOT.Entities.TimeOffPolicies.TimeOffPolicyApprover", b =>
                {
                    b.HasOne("TOT.Entities.EmployeePosition", "EmployeePosition")
                        .WithMany()
                        .HasForeignKey("EmployeePositionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TOT.Entities.TimeOffPolicies.EmployeePositionTimeOffPolicy")
                        .WithMany("Approvers")
                        .HasForeignKey("EmployeePositionTimeOffPolicyId")
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

                    b.HasOne("TOT.Entities.IdentityEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
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

                    b.HasOne("TOT.Entities.IdentityEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}

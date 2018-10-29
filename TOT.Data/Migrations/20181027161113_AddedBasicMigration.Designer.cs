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
    [Migration("20181027161113_AddedBasicMigration")]
    partial class AddedBasicMigration
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

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
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
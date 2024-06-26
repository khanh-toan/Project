﻿// <auto-generated />
using System;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObject.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240217090508_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessObject.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<double>("Hour")
                        .HasColumnType("float");

                    b.Property<double>("OTHour")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("BusinessObject.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("BaseSalary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("DateOffPerYear")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeType")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("InsuranceRate")
                        .HasColumnType("float");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<double>("OTSalaryRate")
                        .HasColumnType("float");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("SalaryType")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TaxRate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LevelId");

                    b.HasIndex("PositionId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("BusinessObject.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CCCD")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsFirstLogin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CCCD")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EmployeeCode")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "HCM",
                            CCCD = "1234567890",
                            CreatedDate = new DateTime(2024, 2, 17, 16, 5, 7, 892, DateTimeKind.Local).AddTicks(9205),
                            Dob = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@projectx.com",
                            EmployeeCode = "SD0001",
                            EmployeeName = "Admin",
                            Gender = 0,
                            IsFirstLogin = true,
                            Password = "$2a$11$XxBpcMNKjUPLyRWJqK5CkOKuhLc6quv2aH/q2jeHmEb6DmdDdMo6i",
                            Phone = "0792123456",
                            Role = 0,
                            Status = 0
                        });
                });

            modelBuilder.Entity("BusinessObject.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Levels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LevelName = "Intern"
                        },
                        new
                        {
                            Id = 2,
                            LevelName = "Fresher"
                        },
                        new
                        {
                            Id = 3,
                            LevelName = "Junior"
                        },
                        new
                        {
                            Id = 4,
                            LevelName = "Senior"
                        },
                        new
                        {
                            Id = 5,
                            LevelName = "Specialized"
                        });
                });

            modelBuilder.Entity("BusinessObject.PayRoll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("BaseSalaryPerHours")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("BaseWorkingHours")
                        .HasColumnType("float");

                    b.Property<decimal>("Bonus")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DayOfHasSalary")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OTSalaryPerHours")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("OTWorkingHours")
                        .HasColumnType("float");

                    b.Property<double>("RealWorkingHours")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("TotalDeductionRate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("Payrolls");
                });

            modelBuilder.Entity("BusinessObject.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PositionName = "Software Engineering"
                        },
                        new
                        {
                            Id = 2,
                            PositionName = "Business Analysis"
                        },
                        new
                        {
                            Id = 3,
                            PositionName = "Automation Tester"
                        },
                        new
                        {
                            Id = 4,
                            PositionName = "Project Manager"
                        },
                        new
                        {
                            Id = 5,
                            PositionName = "Solution Architecture"
                        });
                });

            modelBuilder.Entity("BusinessObject.TakeLeave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeaveDays")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("TakeLeaves");
                });

            modelBuilder.Entity("BusinessObject.Attendance", b =>
                {
                    b.HasOne("BusinessObject.Employee", "User")
                        .WithMany("Attendances")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObject.Contract", b =>
                {
                    b.HasOne("BusinessObject.Employee", "User")
                        .WithMany("Contracts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.Level", "Level")
                        .WithMany("Contracts")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.Position", "Position")
                        .WithMany("Contracts")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");

                    b.Navigation("Position");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObject.PayRoll", b =>
                {
                    b.HasOne("BusinessObject.Contract", "Contract")
                        .WithMany("PayRolls")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("BusinessObject.TakeLeave", b =>
                {
                    b.HasOne("BusinessObject.Employee", "User")
                        .WithMany("TakeLeaves")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObject.Contract", b =>
                {
                    b.Navigation("PayRolls");
                });

            modelBuilder.Entity("BusinessObject.Employee", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Contracts");

                    b.Navigation("TakeLeaves");
                });

            modelBuilder.Entity("BusinessObject.Level", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("BusinessObject.Position", b =>
                {
                    b.Navigation("Contracts");
                });
#pragma warning restore 612, 618
        }
    }
}

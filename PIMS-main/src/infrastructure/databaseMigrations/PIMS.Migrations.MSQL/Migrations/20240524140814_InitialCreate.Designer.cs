﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PIMS.Infrastructure.Persistence.DbContexts;

#nullable disable

namespace PIMS.Migrations.MSQL.Migrations
{
    [DbContext(typeof(PIMSDbContext))]
    [Migration("20240524140814_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.7.23375.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PIMS.Domain.Document", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActiveTaskID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("ActiveTaskID")
                        .IsUnique()
                        .HasFilter("[ActiveTaskID] IS NOT NULL");

                    b.ToTable("Documents", (string)null);
                });

            modelBuilder.Entity("PIMS.Domain.EventLogAggregate.EventLog", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Properties")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserInfo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventLog", (string)null);
                });

            modelBuilder.Entity("PIMS.Domain.ProjectTask", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DocumentID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PreviousTaskID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("DocumentID");

                    b.HasIndex("PreviousTaskID")
                        .IsUnique()
                        .HasFilter("[PreviousTaskID] IS NOT NULL");

                    b.ToTable("ProjectTasks", (string)null);
                });

            modelBuilder.Entity("PIMS.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BlockedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("PIMS.Domain.UserDataAggregate.UserData", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FiredAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("PreventUpdate")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WorkingHours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserData", (string)null);
                });

            modelBuilder.Entity("PIMS.Domain.Document", b =>
                {
                    b.HasOne("PIMS.Domain.ProjectTask", "ActiveTask")
                        .WithOne()
                        .HasForeignKey("PIMS.Domain.Document", "ActiveTaskID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ActiveTask");
                });

            modelBuilder.Entity("PIMS.Domain.ProjectTask", b =>
                {
                    b.HasOne("PIMS.Domain.Document", "Document")
                        .WithMany("Tasks")
                        .HasForeignKey("DocumentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PIMS.Domain.ProjectTask", "PreviousTask")
                        .WithOne()
                        .HasForeignKey("PIMS.Domain.ProjectTask", "PreviousTaskID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Document");

                    b.Navigation("PreviousTask");
                });

            modelBuilder.Entity("PIMS.Domain.UserDataAggregate.UserData", b =>
                {
                    b.HasOne("PIMS.Domain.UserAggregate.User", "User")
                        .WithMany("UserData")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PIMS.Domain.UserDataAggregate.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserDataId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("HomeEmail")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("WorkEmail")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserDataId");

                            b1.ToTable("UserData");

                            b1.WithOwner()
                                .HasForeignKey("UserDataId");
                        });

                    b.OwnsOne("PIMS.Domain.UserDataAggregate.ValueObjects.EmployeeNumber", "EmployeeNumber", b1 =>
                        {
                            b1.Property<Guid>("UserDataId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("UserDataId");

                            b1.ToTable("UserData");

                            b1.WithOwner()
                                .HasForeignKey("UserDataId");
                        });

                    b.OwnsOne("PIMS.Domain.UserDataAggregate.ValueObjects.Phone", "Phone", b1 =>
                        {
                            b1.Property<Guid>("UserDataId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CityPhoneNumber")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("InnerPhoneNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("MobilePhoneNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserDataId");

                            b1.ToTable("UserData");

                            b1.WithOwner()
                                .HasForeignKey("UserDataId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("EmployeeNumber");

                    b.Navigation("Phone")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PIMS.Domain.Document", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("PIMS.Domain.UserAggregate.User", b =>
                {
                    b.Navigation("UserData");
                });
#pragma warning restore 612, 618
        }
    }
}

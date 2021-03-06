// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrackingRemoteHostService.Services.DbService;

namespace TrackingRemoteHostService.Migrations
{
    [DbContext(typeof(EfCoreService))]
    [Migration("20210916165508_UpdateMigration")]
    partial class UpdateMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("TrackingRemoteHostService.Models.AuthUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("auths", "public");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Available")
                        .HasColumnType("boolean")
                        .HasColumnName("available");

                    b.Property<DateTime>("Date")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.Property<int>("UserScheduleId")
                        .HasColumnType("integer")
                        .HasColumnName("userscheduleid");

                    b.HasKey("Id");

                    b.HasIndex("UserScheduleId");

                    b.ToTable("history", "public");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.Host", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("hosts", "public");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("HostId")
                        .HasColumnType("integer")
                        .HasColumnName("hostid");

                    b.Property<int>("Interval")
                        .HasColumnType("integer")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.ToTable("shedules", "public");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("firstname");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("secondname");

                    b.HasKey("Id");

                    b.ToTable("users", "public");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.UserSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ScheduleId")
                        .HasColumnType("integer")
                        .HasColumnName("scheduleId");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("UserId");

                    b.ToTable("userschedule", "public");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.AuthUser", b =>
                {
                    b.HasOne("TrackingRemoteHostService.Models.User", "User")
                        .WithOne("Auth")
                        .HasForeignKey("TrackingRemoteHostService.Models.AuthUser", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.History", b =>
                {
                    b.HasOne("TrackingRemoteHostService.Models.UserSchedule", "UserSchedule")
                        .WithMany("Histories")
                        .HasForeignKey("UserScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSchedule");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.Schedule", b =>
                {
                    b.HasOne("TrackingRemoteHostService.Models.Host", "Host")
                        .WithMany("Shedules")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Host");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.UserSchedule", b =>
                {
                    b.HasOne("TrackingRemoteHostService.Models.Schedule", "Schedule")
                        .WithMany("UserSchedules")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrackingRemoteHostService.Models.User", "User")
                        .WithMany("UserSchedules")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.Host", b =>
                {
                    b.Navigation("Shedules");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.Schedule", b =>
                {
                    b.Navigation("UserSchedules");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.User", b =>
                {
                    b.Navigation("Auth");

                    b.Navigation("UserSchedules");
                });

            modelBuilder.Entity("TrackingRemoteHostService.Models.UserSchedule", b =>
                {
                    b.Navigation("Histories");
                });
#pragma warning restore 612, 618
        }
    }
}

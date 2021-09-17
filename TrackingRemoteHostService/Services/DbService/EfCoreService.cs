using Microsoft.EntityFrameworkCore;
using TrackingRemoteHostService.Models;

namespace TrackingRemoteHostService.Services.DbService
{
    class EfCoreService : DbContext
    {
        public EfCoreService(DbContextOptions<EfCoreService> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthUser>().HasIndex(w => w.Login).IsUnique();

            modelBuilder.Entity<AuthUser>().HasOne(w => w.User).WithOne(w => w.Auth);

            modelBuilder.Entity<Host>().HasIndex(w => w.Url).IsUnique();

            modelBuilder.Entity<Schedule>().HasOne(w => w.Host).WithMany(w => w.Shedules).HasForeignKey(w => w.HostId);

            modelBuilder.Entity<UserSchedule>().HasOne(w => w.Schedule).WithMany(w => w.UserSchedules).HasForeignKey(w => w.ScheduleId);

            modelBuilder.Entity<UserSchedule>().HasOne(w => w.User).WithMany(w => w.UserSchedules).HasForeignKey(w => w.UserId);

            modelBuilder.Entity<History>().HasOne(w => w.Schedule).WithMany(w => w.Histories).HasForeignKey(w => w.ScheduleId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<UserSchedule> UserSchedules { get; set; }
        public DbSet<History> Histories { get; set; }
    }
}

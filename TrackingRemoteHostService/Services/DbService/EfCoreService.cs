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

            modelBuilder.Entity<Schedule>().HasOne(w => w.Host).WithMany(w=>w.Shedules);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Schedule> Shedules { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using TrackingRemoteHostService.Models;

namespace TrackingRemoteHostService.Services.DbService
{
    public class EfCoreService:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthUser>().HasIndex(w => w.Login).IsUnique();

            modelBuilder.Entity<AuthUser>().HasOne(w => w.User).WithOne(w => w.Auth);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
    }
}

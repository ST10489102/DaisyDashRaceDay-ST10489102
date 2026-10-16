using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Core Users
        public DbSet<User> Users { get; set; }

        // RaceDay Core (official POE)
        public DbSet<Race> Races { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Result> Results { get; set; }

        // Your DaisyDash extra (keep them - lecturer sees full system)
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Jockey> Jockeys { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<RaceEntry> RaceEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent decimal Price issues - make double if Ticket has Price
            // If your Ticket model uses decimal, this line fixes it
            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasConversion<double>();
        }
    }
}
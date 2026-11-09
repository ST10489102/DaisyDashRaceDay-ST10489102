<<<<<<< Updated upstream
﻿using Microsoft.EntityFrameworkCore;
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
=======
﻿using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Jockey> Jockeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ticket Price - make double if Ticket has Price
            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasConversion<double>();

            // FIX: Stop SQL Server cascade-cycle error
            // Event -> Categories -> Enrolments -> Event = loop
            // Restrict = don't auto-delete, just block it

            modelBuilder.Entity<Category>()
                .HasOne(c => c.Event)
                .WithMany()
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrolment>()
                .HasOne(e => e.Event)
                .WithMany()
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrolment>()
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrolment>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Enrolment)
                .WithMany()
                .HasForeignKey(r => r.EnrolmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Event)
                .WithMany()
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
>>>>>>> Stashed changes
}
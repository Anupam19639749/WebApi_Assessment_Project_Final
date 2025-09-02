using Microsoft.EntityFrameworkCore;
using WebApi_Assessment_Project_Final.Models;

namespace WebApi_Assessment_Project_Final.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Unique Email for Participant
            modelBuilder.Entity<Participant>()
                .HasIndex(p => p.Email)
                .IsUnique();

            // Configuring Many-to-Many: Session <-> Participant
            modelBuilder.Entity<Session>()
                .HasMany(s => s.Participants)
                .WithMany(p => p.Sessions)
                .UsingEntity(j => j.ToTable("SessionParticipants"));

            // Configuring One-to-Many: Event -> Sessions
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Sessions)
                .WithOne(s => s.Event)
                .HasForeignKey(s => s.EventId)
                .IsRequired();
        }
    }
}

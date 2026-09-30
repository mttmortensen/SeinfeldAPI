using Microsoft.EntityFrameworkCore;
using SeinfeldAPI.Models;

namespace SeinfeldAPI.Data
{
    public class SeinfeldDbContext : DbContext
    {
        // Constructor lets EF use options like the connection string
        public SeinfeldDbContext(DbContextOptions<SeinfeldDbContext> options)
            : base(options)
        { }

        // This tells EF: "Create a table names Episodes using the Episode Model"
        public DbSet<Episode> Episodes { get; set; }

        // Same thing here: This maps the EpisodeQuotes model to the SQL Table
        public DbSet<EpisodeQuotes> EpisodeQuotes { get; set; }

        // We're now creating a table for EF to use for Users domain
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Only one episode per season + episode number
            // Matches SQL/add_episodes_unique_season_episode.sql
            modelBuilder.Entity<Episode>()
                .HasIndex(e => new { e.Season, e.EpisodeNumber })
                .IsUnique();
        }

    }
}

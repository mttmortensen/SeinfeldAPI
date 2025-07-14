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

    }
}

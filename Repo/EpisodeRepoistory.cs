using Microsoft.EntityFrameworkCore;
using SeinfeldAPI.Data;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;

namespace SeinfeldAPI.Repo
{
    public class EpisodeRepoistory : IEpisodeRepository
    {
        // Holds a reference to the db for querying 
        private readonly SeinfeldDbContext _context;

        // Constructor: sets the _context when the repo is created 
        public EpisodeRepoistory(SeinfeldDbContext context) 
        {
            _context = context;
        }

        // Returns all Episodes, including their quotes (EpisodeQuotes)
        public List<Episode> GetAllEpisodes()
        {
            return _context.Episodes
                .Include(e => e.Quotes)
                .ToList();
        }
    }
}

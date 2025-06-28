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

        // Returns a single Episode by it's Id (null if not found, 'Episode?') 
        public Episode? GetEpisodeById(int id) 
        {
            return _context.Episodes
                .Include(e => e.Quotes)
                .FirstOrDefault();
        }


        // ==== These haven't been made yet in _context
        // ==== They're just inhireting the methods from DbContext Interface

        // Adds a new option to the database
        public void AddEpisode(Episode episode) 
        {
            _context.Episodes.Add(episode);
        }

        // Marks an existing episode for update
        public void UpdateEpisode(Episode episode) 
        {
            _context.Episodes.Update(episode);
        }
    }
}

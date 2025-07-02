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
                .FirstOrDefault(e => e.Id == id);
        }

        // Adds a new option to the database
        public bool AddEpisode(Episode episode) 
        {
            _context.Episodes.Add(episode);
            return _context.SaveChanges() > 0;
        }

        // Marks an existing episode for update
        public bool UpdateEpisode(Episode episode) 
        {
            _context.Episodes.Update(episode);
            return _context.SaveChanges() > 0;
        }

        // Finds and removes an episode by Id
        public bool DeleteEpisode(int id) 
        {
            var episode = _context.Episodes.Find(id);

            if (episode != null)
            {
                _context.Episodes.Remove(episode);
                return _context.SaveChanges() > 0;
            }

            return false;
        }

        // Saves all changes made so far (post/update/delete)
        public bool SaveChanges() 
        {
            //returns true if something changed
            return _context.SaveChanges() > 0; 
        }
    }
}

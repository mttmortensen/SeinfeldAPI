using Microsoft.EntityFrameworkCore;
using SeinfeldAPI.Data;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;

namespace SeinfeldAPI.Repo
{
    public class EpisodeQuotesRepoistory: IEpisodeQuotesRepository
    {
        // Reference to the db via the context
        private readonly SeinfeldDbContext _context;

        // Construct injecting the context
        public EpisodeQuotesRepoistory(SeinfeldDbContext context) 
        {
            _context = context;
        }

        // Return all quotes in the db (regardless of episode)
        public List<EpisodeQuotes> GetAllQuotes()
        {
            return _context.EpisodeQuotes
                // This will bring in all the episode date with it
                // We will use the EQ DTO object to trim this down
                .Include(q => q.Episode) 
                .ToList();
        }

        // Return all quotes for a specific episode
        public List<EpisodeQuotes> GetQuotesForEpisode(int episodeId) 
        {
            return _context.EpisodeQuotes
                // I still need to bring in the specific episode 
                // that's why .Where() is still here
                .Where(q => q.EpisodeId == episodeId)
                .Include(q => q.Episode)
                .ToList();
        }

        // Return a single quote by it's Id
        public EpisodeQuotes? GetQuoteById(int id) 
        {
            return _context.EpisodeQuotes
                .Include(q => q.Episode)
                .FirstOrDefault(q => q.Id == id);
        }

        // Add a new quote (not saved yet)
        public bool AddQuote(EpisodeQuotes quotes) 
        {
            _context.EpisodeQuotes.Add(quotes);
            return _context.SaveChanges() > 0;
        }

        // Mark a quote for update
        public bool UpdateQuote(EpisodeQuotes quote)
        {
            _context.EpisodeQuotes.Update(quote);
            return _context.SaveChanges() > 0;

        }

        // Remove a quote by ID (if it exists)
        public bool DeleteQuote(int id)
        {
            var quote = _context.EpisodeQuotes.Find(id);
            if (quote != null)
            {
                _context.EpisodeQuotes.Remove(quote);
                return _context.SaveChanges() > 0;
            }

            return false;
        }

        // Saves all changes made so far (post/update/delete)

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }

}

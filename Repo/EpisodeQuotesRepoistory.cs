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
                .Include(q => q.Episode) // This will bring in all the episode date with it, might wanna trim this down later
                .ToList();
        }

        // Return all quotes for a specific episode
        public List<EpisodeQuotes> GetQuotesForEpisode(int episodeId) 
        {
            return _context.EpisodeQuotes
                .Where(q => q.EpisodeId == episodeId)
                .ToList();
        }

        // Return a single quote by it's Id
        public EpisodeQuotes? GetQuoteById(int id) 
        {
            return _context.EpisodeQuotes
                .Include(q => q.Episode)
                .FirstOrDefault(q => q.Id == id);
        }


        // ==== These haven't been made yet in _context
        // ==== They're just inheriting the methods from DbContext Interface

        // Add a new quote (not saved yet)
        public void AddQuote(EpisodeQuotes quotes) 
        {
            _context.EpisodeQuotes.Add(quotes);
        }

        // Mark a quote for update
        public void UpdateQuote(EpisodeQuotes quote)
        {
            _context.EpisodeQuotes.Update(quote);
        }

        // Remove a quote by ID (if it exists)
        public void DeleteQuote(int id)
        {
            var quote = _context.EpisodeQuotes.Find(id);
            if (quote != null)
                _context.EpisodeQuotes.Remove(quote);
        }

        // Saves all changes made so far (post/update/delete)

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }

}
}

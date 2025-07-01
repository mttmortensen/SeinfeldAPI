using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;
using SeinfeldAPI.Repo;

namespace SeinfeldAPI.Services
{
    public class EpisodeQuotesService : IEpisodeQuotesService
    {
        private readonly IEpisodeQuotesRepository _quotesRepo;
        private readonly IEpisodeRepository _episodeRepo;

        public EpisodeQuotesService(IEpisodeQuotesRepository quotesRepo, IEpisodeRepository episodeRepo) 
        {
            _quotesRepo = quotesRepo;
            _episodeRepo = episodeRepo;
        }

        // Get all quotes from all episodes
        public List<EpisodeQuotes> GetAllQuotes()
        {
            return _quotesRepo.GetAllQuotes();
        }

        // Get all quotes for a specific episode
        public List<EpisodeQuotes> GetQuotesForEpisode(int episodeId)
        {
            return _quotesRepo.GetQuotesForEpisode(episodeId);
        }

        // Get a single quote by ID
        public EpisodeQuotes? GetQuoteById(int id)
        {
            return _quotesRepo.GetQuoteById(id);
        }

        // Add a new quote (only if the episode exists)
        public bool AddQuote(EpisodeQuotes quote)
        {
            var episodeExists = _episodeRepo.GetEpisodeById(quote.EpisodeId) != null;
            if (!episodeExists)
                return false;

            _quotesRepo.AddQuote(quote);
            return _quotesRepo.SaveChanges();
        }

        // Update an existing quote
        public bool UpdateQuote(EpisodeQuotes quote)
        {
            _quotesRepo.UpdateQuote(quote);
            return _quotesRepo.SaveChanges();
        }

        // Delete a quote by ID
        public bool DeleteQuote(int id)
        {
            _quotesRepo.DeleteQuote(id);
            return _quotesRepo.SaveChanges();
        }
    }
}

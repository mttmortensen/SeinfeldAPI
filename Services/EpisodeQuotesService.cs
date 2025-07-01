using SeinfeldAPI.Models;
using SeinfeldAPI.Repo;

namespace SeinfeldAPI.Services
{
    public class EpisodeQuotesService
    {
        private readonly EpisodeQuotesRepoistory _quotesRepo;
        private readonly EpisodeRepoistory _episodeRepo;

        public EpisodeQuotesService(EpisodeQuotesRepoistory quotesRepo, EpisodeRepoistory episodeRepo) 
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
    }
}

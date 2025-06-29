using SeinfeldAPI.Models;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeQuotesRepository
    {

        List<EpisodeQuotes> GetAllQuotes();
        List<EpisodeQuotes> GetQuotesForEpisode(int episodeId);
        EpisodeQuotes? GetQuoteById(int id);

        // These methods wouldn't be used right away
        // But just declaring them for now

        void AddQuote(EpisodeQuotes quote);
        void UpdateQuote(EpisodeQuotes quote);
        void DeleteQuote(int id);
        bool SaveChanges();
    }
}

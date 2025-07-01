using SeinfeldAPI.Models;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeQuotesRepository
    {
        List<EpisodeQuotes> GetAllQuotes();
        List<EpisodeQuotes> GetQuotesForEpisode(int episodeId);
        EpisodeQuotes? GetQuoteById(int id);
        bool AddQuote(EpisodeQuotes quote);
        bool UpdateQuote(EpisodeQuotes quote);
        bool DeleteQuote(int id);
        bool SaveChanges();
    }
}

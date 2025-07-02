using SeinfeldAPI.Models;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeQuotesService
    {
        List<EpisodeQuoteDto> GetAllQuotes();
        List<EpisodeQuoteDto> GetQuotesForEpisode(int episodeId);
        EpisodeQuoteDto? GetQuoteById(int id);
        bool AddQuote(EpisodeQuoteDto quote);
        bool UpdateQuote(EpisodeQuoteDto quote);
        bool DeleteQuote(int id);
    }
}

using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeQuotesService
    {
        List<QuoteCreateDto> GetAllQuotes();
        List<QuoteCreateDto> GetQuotesForEpisode(int episodeId);
        QuoteCreateDto? GetQuoteById(int id);
        bool AddQuote(QuoteCreateDto quote);
        bool UpdateQuote(QuoteCreateDto quote);
        bool DeleteQuote(int id);
    }
}

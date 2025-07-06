using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeQuotesService
    {
        List<QuoteCreateOrUpdateDto> GetAllQuotes();
        List<QuoteCreateOrUpdateDto> GetQuotesForEpisode(int episodeId);
        QuoteCreateOrUpdateDto? GetQuoteById(int id);
        bool AddQuote(QuoteCreateOrUpdateDto quote);
        bool UpdateQuote(QuoteCreateOrUpdateDto quote);
        bool DeleteQuote(int id);
    }
}

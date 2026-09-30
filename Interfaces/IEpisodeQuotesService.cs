using SeinfeldAPI.Models;
using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeQuotesService
    {
        List<QuoteCreateDto> GetAllQuotes();
        List<QuoteCreateDto> GetQuotesForEpisode(int episodeId);
        QuoteCreateDto? GetQuoteById(int id);
        bool AddQuote(QuoteCreateDto quote);
        bool UpdateQuote(int id, QuoteUpdateDto quote);
        bool DeleteQuote(int id);

        // Quote entry frontend
        List<QuoteEntryDto> GetRecentQuotes(int limit);
        (QuoteEntryStatus Status, QuoteEntryDto? Quote) AddQuoteEntry(QuoteEntryCreateDto quote);
        QuoteEntryStatus UpdateQuoteEntry(int id, QuoteEntryUpdateDto quote);
    }
}

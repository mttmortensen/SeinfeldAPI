using SeinfeldAPI.Models;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeQuotesService
    {
        // Get all quotes from all episodes
        public void GetAllQuotes()
        {}

        // Get all quotes for a specific episode
        public void GetQuotesForEpisode(int episodeId)
        {}

        // Get a single quote by ID
        public void GetQuoteById(int id)
        {}

        // Add a new quote (only if the episode exists)
        public void AddQuote(EpisodeQuotes quote)
        {}

        // Update an existing quote
        public void UpdateQuote(EpisodeQuotes quote)
        {}

        // Delete a quote by ID
        public void DeleteQuote(int id)
        {}
    }
}

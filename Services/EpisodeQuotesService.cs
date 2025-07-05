using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;
using SeinfeldAPI.Models.DTOs;
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
        public List<EpisodeQuoteDto> GetAllQuotes()
        {
            return _quotesRepo.GetAllQuotes()
                .Select(q => new EpisodeQuoteDto 
                {
                    Id = q.Id,
                    Quote = q.Quote,
                    Character = q.Character,
                    EpisodeId = q.EpisodeId,
                    EpisodeTitle = q.Episode.Title,
                    EpisodeSeason = q.Episode.Season
                })
                .ToList();
        }

        // Get all quotes for a specific episode
        public List<EpisodeQuoteDto> GetQuotesForEpisode(int episodeId)
        {
            return _quotesRepo.GetQuotesForEpisode(episodeId)
                .Select(q => new EpisodeQuoteDto 
                {
                    Id = q.Id,
                    Quote = q.Quote,
                    Character = q.Character,
                    EpisodeId = q.EpisodeId,
                    EpisodeTitle = q.Episode.Title,
                    EpisodeSeason = q.Episode.Season
                })
                .ToList();
        }

        // Get a single quote by ID
        public EpisodeQuoteDto? GetQuoteById(int id)
        {
            EpisodeQuotes quote = _quotesRepo.GetQuoteById(id);

            if (quote == null || quote.Episode == null)
                return null;

            return new EpisodeQuoteDto
            {
                Id = quote.Id,
                Quote = quote.Quote,
                Character = quote.Character,
                EpisodeId = quote.EpisodeId,
                EpisodeTitle = quote.Episode.Title,
                EpisodeSeason = quote.Episode.Season
            };
        }

        // Add a new quote (only if the episode exists)
        public bool AddQuote(EpisodeQuoteDto quoteDto)
        {
            int? episodeId = ResolveEpisodeId(quoteDto);
            if (episodeId == null)
                return false;

            var quote = new EpisodeQuotes
            {
                Quote = quoteDto.Quote,
                Character = quoteDto.Character,
                EpisodeId = episodeId.Value
            };

            _quotesRepo.AddQuote(quote);
            return _quotesRepo.SaveChanges();
        }

        // Update an existing quote
        public bool UpdateQuote(EpisodeQuoteDto quoteDto)
        {
            EpisodeQuotes existing = _quotesRepo.GetQuoteById(quoteDto.Id);
            if (existing == null)
                return false;

            int? episodeId = ResolveEpisodeId(quoteDto);
            if (episodeId == null)
                return false;

            existing.Quote = quoteDto.Quote;
            existing.Character = quoteDto.Character;
            existing.EpisodeId = episodeId.Value;

            _quotesRepo.UpdateQuote(existing);
            return _quotesRepo.SaveChanges();
        }

        // Delete a quote by ID
        public bool DeleteQuote(int id)
        {
            _quotesRepo.DeleteQuote(id);
            return _quotesRepo.SaveChanges();
        }

        // Resolves EpisodeId from either direct Id or from Title + Season
        private int? ResolveEpisodeId(EpisodeQuoteDto dto) 
        {
            // Case 1: EpisodeId is provided directly
            if (dto.EpisodeId.HasValue)
                return dto.EpisodeId.Value;

            // Case 2: Try to resolve by title and season
            if (!string.IsNullOrWhiteSpace(dto.EpisodeTitle) &&
                !string.IsNullOrWhiteSpace(dto.EpisodeSeason))
            {
                Episode match = _episodeRepo.GetAllEpisodes()
                    .FirstOrDefault(e =>
                        e.Title.Equals(dto.EpisodeTitle, StringComparison.OrdinalIgnoreCase) &&
                        e.Season.Equals(dto.EpisodeSeason, StringComparison.OrdinalIgnoreCase));

                return match?.Id;
            }

            // Not enough info to resolve
            return null;
        }
    }
}

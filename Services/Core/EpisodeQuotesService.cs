using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;
using SeinfeldAPI.Models.DTOs;
using SeinfeldAPI.Repo;

namespace SeinfeldAPI.Services.Core
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
        public List<QuoteCreateDto> GetAllQuotes()
        {
            return _quotesRepo.GetAllQuotes()
                .Select(q => new QuoteCreateDto 
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
        public List<QuoteCreateDto> GetQuotesForEpisode(int episodeId)
        {
            return _quotesRepo.GetQuotesForEpisode(episodeId)
                .Select(q => new QuoteCreateDto 
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
        public QuoteCreateDto? GetQuoteById(int id)
        {
            EpisodeQuotes quote = _quotesRepo.GetQuoteById(id);

            if (quote == null || quote.Episode == null)
                return null;

            return new QuoteCreateDto
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
        public bool AddQuote(QuoteCreateDto quoteDto)
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
        public bool UpdateQuote(int Id, QuoteUpdateDto quoteDto)
        {
            EpisodeQuotes existing = _quotesRepo.GetQuoteById(Id);
            if (existing == null)
                return false;

            // Only update Quote if provided
            if (!string.IsNullOrWhiteSpace(quoteDto.Quote))
                existing.Quote = quoteDto.Quote;

            // Only update Character if provided
            if (!string.IsNullOrWhiteSpace(quoteDto.Character))
                existing.Character = quoteDto.Character;

            // Only update EpisodeId if EpisodeId or Title+Season was provided
            bool shouldTryUpdateEpisode =
                quoteDto.EpisodeId.HasValue ||
                !string.IsNullOrWhiteSpace(quoteDto.EpisodeTitle) &&
                 !string.IsNullOrWhiteSpace(quoteDto.EpisodeSeason);

            if (shouldTryUpdateEpisode)
            {
                int? episodeId = ResolveEpisodeId(quoteDto);
                if (episodeId == null)
                    return false;

                existing.EpisodeId = episodeId.Value;
            }

            _quotesRepo.UpdateQuote(existing);
            return _quotesRepo.SaveChanges();
        }

        // Delete a quote by ID
        public bool DeleteQuote(int id)
        {
            // Repo returns false when the quote doesn't exist
            return _quotesRepo.DeleteQuote(id);
        }

        // Get the most recently added quotes (for the quote entry frontend)
        public List<QuoteEntryDto> GetRecentQuotes(int limit)
        {
            return _quotesRepo.GetRecentQuotes(limit)
                .Select(ToQuoteEntryDto)
                .ToList();
        }

        // Add a quote by Season + EpisodeNumber
        // Creates the episode (with EpisodeTitle) if it doesn't exist yet
        // Rejects the same quote text on the same episode
        public (QuoteEntryStatus Status, QuoteEntryDto? Quote) AddQuoteEntry(QuoteEntryCreateDto quoteDto)
        {
            string season = quoteDto.Season.ToString();
            string episodeNumber = quoteDto.EpisodeNumber.ToString();
            string text = quoteDto.Text.Trim();

            var quote = new EpisodeQuotes
            {
                Quote = text,
                Character = quoteDto.Speaker.Trim()
            };

            Episode episode = _episodeRepo.GetEpisodeBySeasonAndNumber(season, episodeNumber);

            if (episode == null)
            {
                // New episode, so we need a title for it
                if (string.IsNullOrWhiteSpace(quoteDto.EpisodeTitle))
                    return (QuoteEntryStatus.EpisodeTitleRequired, null);

                episode = new Episode
                {
                    Title = quoteDto.EpisodeTitle.Trim(),
                    Season = season,
                    EpisodeNumber = episodeNumber,
                    Quotes = new List<EpisodeQuotes> { quote }
                };
                quote.Episode = episode;

                // Saves the episode and the quote together
                _episodeRepo.AddEpisode(episode);
            }
            else
            {
                if (_quotesRepo.QuoteExists(episode.Id, text))
                    return (QuoteEntryStatus.Duplicate, null);

                quote.EpisodeId = episode.Id;
                quote.Episode = episode;
                _quotesRepo.AddQuote(quote);
            }

            return (QuoteEntryStatus.Success, ToQuoteEntryDto(quote));
        }

        // Update the text and speaker of a quote
        public QuoteEntryStatus UpdateQuoteEntry(int id, QuoteEntryUpdateDto quoteDto)
        {
            EpisodeQuotes existing = _quotesRepo.GetQuoteById(id);
            if (existing == null)
                return QuoteEntryStatus.NotFound;

            string text = quoteDto.Text.Trim();

            if (_quotesRepo.QuoteExists(existing.EpisodeId, text, excludeId: id))
                return QuoteEntryStatus.Duplicate;

            existing.Quote = text;
            existing.Character = quoteDto.Speaker.Trim();

            _quotesRepo.UpdateQuote(existing);
            return QuoteEntryStatus.Success;
        }

        // Maps a quote (with its Episode loaded) to the quote entry Dto
        private static QuoteEntryDto ToQuoteEntryDto(EpisodeQuotes q)
        {
            int.TryParse(q.Episode.Season, out int season);
            int.TryParse(q.Episode.EpisodeNumber, out int episodeNumber);

            return new QuoteEntryDto
            {
                Id = q.Id,
                Text = q.Quote,
                Speaker = q.Character,
                EpisodeId = q.EpisodeId,
                Season = season,
                EpisodeNumber = episodeNumber,
                EpisodeTitle = q.Episode.Title
            };
        }

        // Resolves EpisodeId from either direct Id or from Title + Season
        private int? ResolveEpisodeId(IEpisodeResolvable dto) 
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

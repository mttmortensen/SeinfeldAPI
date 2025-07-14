using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;
using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Services.Core
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IEpisodeRepository _episodeRepo;
        private readonly IEpisodeQuotesService _eqService;

        public EpisodeService(IEpisodeRepository episodeRepo, IEpisodeQuotesService eqService)
        {
            _episodeRepo = episodeRepo;
            _eqService = eqService;
        }

        // Get all episodes
        public List<EpisodeWithQuotesDto> GetAllEpisodes()
        {
            return _episodeRepo.GetAllEpisodes()
                .Select(e => new EpisodeWithQuotesDto 
                {
                    Id = e.Id,
                    Title = e.Title,
                    Season = e.Season,
                    EpisodeNumber = e.EpisodeNumber,
                    AirDate = e.AirDate,
                    Quotes = e.Quotes.Select(q => new QuoteInlineDto
                    {
                        Id = q.Id,
                        Quote = q.Quote,
                        Character = q.Character,
                        EpisodeId = e.Id
                    }).ToList()
                })
                .ToList();
        }

        // Get a specific episode by ID
        public EpisodeWithQuotesDto? GetEpisodeById(int id)
        {
            Episode episode = _episodeRepo.GetEpisodeById(id);

            if (episode == null)
                return null;

            return new EpisodeWithQuotesDto
            {
                Id = episode.Id,
                Title = episode.Title,
                Season = episode.Season,
                EpisodeNumber = episode.EpisodeNumber,
                AirDate = episode.AirDate,
                Quotes = episode.Quotes.Select(q => new QuoteInlineDto
                {
                    Id = q.Id,
                    Quote = q.Quote,
                    Character = q.Character,
                    EpisodeId = episode.Id
                }).ToList()
            };
        }

        // Add a new episode
        public EpisodeWithQuotesDto? AddEpisode(EpisodeWithQuotesDto episodeDto)
        {
            Episode episode = new Episode
            {
                Title = episodeDto.Title,
                Season = episodeDto.Season,
                EpisodeNumber = episodeDto.EpisodeNumber,
                AirDate = episodeDto.AirDate,
                Quotes = episodeDto.Quotes?.Select(q => new EpisodeQuotes
                {
                    Quote = q.Quote,
                    Character = q.Character
                }).ToList() ?? new List<EpisodeQuotes>()
            };

            foreach (var quote in episode.Quotes)
                quote.Episode = episode;

            _episodeRepo.AddEpisode(episode);
            bool success = _episodeRepo.SaveChanges();

            if (!success)
                return null;

            // Map to DTO here and return
            return new EpisodeWithQuotesDto
            {
                Title = episode.Title,
                Season = episode.Season,
                EpisodeNumber = episode.EpisodeNumber,
                AirDate = episode.AirDate,
                Quotes = episode.Quotes.Select(q => new QuoteInlineDto
                {
                    Quote = q.Quote,
                    Character = q.Character
                }).ToList()
            };
        }



        // Update an existing episode
        public bool UpdateEpisode(EpisodeUpdateDto episodeFlatDto)
        {
            // Checks to see if the Raw Episode exists 
            // With the EpisdoeFlatDTO Id
            Episode exisiting = _episodeRepo.GetEpisodeById(episodeFlatDto.Id);
            if (exisiting == null)
                return false;

            // I'm going to manually update them here 
            // By doing these conditions, I can allow for empty values to be allowed without breaking the app 
            // Doing empty values will indicate nothing needed to be changed for it.
            if (!string.IsNullOrWhiteSpace(episodeFlatDto.Title))
                exisiting.Title = episodeFlatDto.Title;

            if (!string.IsNullOrWhiteSpace(episodeFlatDto.Season))
                exisiting.Season = episodeFlatDto.Season;

            if (!string.IsNullOrWhiteSpace(episodeFlatDto.EpisodeNumber))
                exisiting.EpisodeNumber = episodeFlatDto.EpisodeNumber;

            if (episodeFlatDto.AirDate.HasValue)
                exisiting.AirDate = episodeFlatDto.AirDate.Value;

            _episodeRepo.UpdateEpisode(exisiting);
            return _episodeRepo.SaveChanges();
        }

        // Delete an episode by ID
        public bool DeleteEpisode(int id)
        {
            _episodeRepo.DeleteEpisode(id);
            return _episodeRepo.SaveChanges();
        }
    }
}

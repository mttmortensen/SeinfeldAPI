using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;
using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IEpisodeRepository _episodeRepo;

        public EpisodeService(IEpisodeRepository episodeRepo)
        {
            _episodeRepo = episodeRepo;
        }

        // Get all episodes
        public List<EpisodeDto> GetAllEpisodes()
        {
            return _episodeRepo.GetAllEpisodes()
                .Select(e => new EpisodeDto 
                {
                    Id = e.Id,
                    Title = e.Title,
                    Season = e.Season,
                    EpisodeNumber = e.EpisodeNumber,
                    AirDate = e.AirDate,
                    Quotes = e.Quotes.Select(q => new EpisodeQuoteDto
                    {
                        Id = q.Id,
                        Quote = q.Quote,
                        Character = q.Character
                    }).ToList()
                })
                .ToList();
        }

        // Get a specific episode by ID
        public EpisodeDto? GetEpisodeById(int id)
        {
            Episode episode = _episodeRepo.GetEpisodeById(id);

            if (episode == null)
                return null;

            return new EpisodeDto 
            {
                Id = episode.Id,
                Title = episode.Title,
                Season = episode.Season,
                EpisodeNumber = episode.EpisodeNumber,
                AirDate = episode.AirDate,
                Quotes = episode.Quotes.Select(q => new EpisodeQuoteDto
                {
                    Id = q.Id,
                    Quote = q.Quote,
                    Character = q.Character
                }).ToList()
            };
        }

        // Add a new episode
        public bool AddEpisode(EpisodeDto episodeDto)
        {

            // Raw Episode is still needed for the repo
            Episode episode = new Episode 
            {
                Id = episodeDto.Id,
                Title = episodeDto.Title,
                Season = episodeDto.Season,
                EpisodeNumber = episodeDto.EpisodeNumber,
                AirDate = episodeDto.AirDate,
                // Since Quotes was another raw C# class 
                // We're needing to convert it from Dto to Raw for EQs
                Quotes = episodeDto.Quotes?.Select(q => new EpisodeQuotes 
                {
                    Quote = q.Quote,
                    Character = q.Character,
                    EpisodeId = episodeDto.Id
                }).ToList() ?? new List<EpisodeQuotes>() // This is handling the null value which we will wanna target better soon

            };

            _episodeRepo.AddEpisode(episode);
            return _episodeRepo.SaveChanges();
        }

        // Update an existing episode
        public bool UpdateEpisode(EpisodeDto episodeDto)
        {
            // Checks to see if the Raw Episode exists 
            // With the EpisdoeDTO Id
            Episode exisiting = _episodeRepo.GetEpisodeById(episodeDto.Id);
            if (exisiting == null)
                return false;

            // I'm going to manually update them here 
            exisiting.Title = episodeDto.Title;
            exisiting.Season = episodeDto.Season;
            exisiting.EpisodeNumber = episodeDto.EpisodeNumber;
            exisiting.AirDate = episodeDto.AirDate;

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

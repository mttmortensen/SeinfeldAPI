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
        public Episode? GetEpisodeById(int id)
        {
            return _episodeRepo.GetEpisodeById(id);
        }

        // Add a new episode
        public bool AddEpisode(Episode episode)
        {
            _episodeRepo.AddEpisode(episode);
            return _episodeRepo.SaveChanges();
        }

        // Update an existing episode
        public bool UpdateEpisode(Episode episode)
        {
            _episodeRepo.UpdateEpisode(episode);
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

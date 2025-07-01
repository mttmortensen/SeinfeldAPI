using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;

namespace SeinfeldAPI.Services
{
    public class EpisodeService
    {
        private readonly IEpisodeRepository _episodeRepo;

        public EpisodeService(IEpisodeRepository episodeRepo)
        {
            _episodeRepo = episodeRepo;
        }

        // Get all episodes
        public List<Episode> GetAllEpisodes()
        {
            return _episodeRepo.GetAllEpisodes();
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

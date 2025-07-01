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
    }
}

using SeinfeldAPI.Models;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeService
    {
        // Get all episodes
        public void GetAllEpisodes()
        {}

        // Get a specific episode by ID
        public void GetEpisodeById(int id)
        {}

        // Add a new episode
        public void AddEpisode(Episode episode)
        {}

        // Update an existing episode
        public void UpdateEpisode(Episode episode)
        {}

        // Delete an episode by ID
        public void DeleteEpisode(int id)
        {}
    }
}

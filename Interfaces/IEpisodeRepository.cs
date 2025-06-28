using SeinfeldAPI.Models;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeRepository
    {
        List<Episode> GetAllEpisodes();
        Episode? GetEpisodeById(int id);

        // These methods wouldn't be used right away
        // But just declaring them for now
        void AddEpisode(Episode episode);
        void UpdateEpisode(Episode episode);
        void DeleteEpisode(int id);
        bool SaveChanges();

    }
}

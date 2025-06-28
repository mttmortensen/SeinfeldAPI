using SeinfeldAPI.Models;

namespace SeinfeldAPI.Repo
{
    public interface IEpisodeRepository
    {
        List<Episode> GetAllEpisodes();
        Episode? GetEpisodeById(int id);

        // These methods would be used right away
        // But just declaring them for now
        void AddEpisode(Episode episode);
        void UpdateEpisode(Episode episode);
        void DeleteEpisode(int id);
        bool SaveChanges();

    }
}

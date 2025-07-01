using SeinfeldAPI.Models;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeService
    {
        List<Episode> GetAllEpisodes();
        Episode? GetEpisodeById(int id);
        bool AddEpisode(Episode episode);
        bool UpdateEpisode(Episode episode);
        bool DeleteEpisode(int id);
    }
}

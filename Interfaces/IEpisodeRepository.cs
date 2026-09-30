using SeinfeldAPI.Models;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeRepository
    {
        List<Episode> GetAllEpisodes();
        Episode? GetEpisodeById(int id);
        Episode? GetEpisodeBySeasonAndNumber(string season, string episodeNumber);
        bool AddEpisode(Episode episode);
        bool UpdateEpisode(Episode episode);
        bool DeleteEpisode(int id);
        bool SaveChanges();

    }
}

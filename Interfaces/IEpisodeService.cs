using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeService
    {
        List<EpisodeDto> GetAllEpisodes();
        EpisodeDto? GetEpisodeById(int id);
        bool AddEpisode(EpisodeDto episode);
        bool UpdateEpisode(EpisodeDto episode);
        bool DeleteEpisode(int id);
    }
}

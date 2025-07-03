using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeService
    {
        List<EpisodeDto> GetAllEpisodes();
        EpisodeDto? GetEpisodeById(int id);
        EpisodeDto? AddEpisode(EpisodeDto episode);
        bool UpdateEpisode(EpisodeFlatDto episode);
        bool DeleteEpisode(int id);
    }
}

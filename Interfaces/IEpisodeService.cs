using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeService
    {
        List<EpisodeWithQuotesDto> GetAllEpisodes();
        EpisodeWithQuotesDto? GetEpisodeById(int id);
        EpisodeWithQuotesDto? AddEpisode(EpisodeWithQuotesDto episode);
        bool UpdateEpisode(EpisodeUpdateDto episode);
        bool DeleteEpisode(int id);
    }
}

namespace SeinfeldAPI.Interfaces
{
    public interface IEpisodeResolvable
    {
        int? EpisodeId { get; }
        string? EpisodeTitle { get; }
        string? EpisodeSeason { get; }
    }
}

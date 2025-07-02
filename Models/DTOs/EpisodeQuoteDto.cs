namespace SeinfeldAPI.Models.DTOs
{
    public class EpisodeQuoteDto
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string Character { get; set; }
        public int EpisodeId { get; set; }
        public string EpisodeTitle { get; set; }
        public string EpisodeSeason { get; set; }
    }
}

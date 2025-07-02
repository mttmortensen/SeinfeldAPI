namespace SeinfeldAPI.Models.DTOs
{
    public class EpisodeQuoteFlatDto
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string Character { get; set; }
        public int EpisodeId { get; set; }
    }
}

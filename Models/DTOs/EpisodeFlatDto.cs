namespace SeinfeldAPI.Models.DTOs
{
    public class EpisodeFlatDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Season { get; set; }
        public string? EpisodeNumber { get; set; }
        public DateTime? AirDate { get; set; }
    }
}

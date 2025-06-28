namespace SeinfeldAPI.Models
{
    public class EpisodeQuotes
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string Character { get; set; }

        // FK
        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
    }
}

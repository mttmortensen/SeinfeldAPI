namespace SeinfeldAPI.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Season { get; set; }
        public string EpisodeNumber { get; set; }
        public string AirDate { get; set; }
        public Dictionary<string, string> Quotes { get; set; }
    }
}

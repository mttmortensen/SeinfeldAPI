namespace SeinfeldAPI.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Season { get; set; }
        public string EpisodeNumber { get; set; }
        public DateTime AirDate { get; set; }
        
        // 1 episode --> many quotes
        public List<EpisodeQuotes> Quotes { get; set; }
    }
}

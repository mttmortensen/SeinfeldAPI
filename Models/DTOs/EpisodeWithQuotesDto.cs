namespace SeinfeldAPI.Models.DTOs
{
    public class EpisodeWithQuotesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Season { get; set; }
        public string EpisodeNumber { get; set; }
        public DateTime AirDate { get; set; }

        // Including now the simplifed EpisodeQuotes data
        public List<QuoteInlineDto> Quotes { get; set; }
    }
}
